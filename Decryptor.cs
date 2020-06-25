using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model_Splitter
{
	class Decryptor
	{
		/**
		 * @brief Reads the current model and precache its materials.
		 *
		 * @param model     The model path.
		 * @return          The root folder and the array list of appropriate files.
		 **/
		public static (string root, ArrayList list) ReadFile(string model)
		{
			// Initialize variables
			string root; string path; string material; int numMat; byte ch; ArrayList list = new ArrayList(); list.Add(model);
			string[] types = new string[] { ".sw.vtx", ".dx80.vtx", ".dx90.vtx", ".phy", ".vvd" }; 

			// Finds the first occurrence of a character in a string
			int format = model.LastIndexOf('.');

			// i = resource type
			for (int i = 0; i < 5; i++)
			{
				// Extract value string
				string resource = model.Substring(0, format) + types[i];

				// Validate resource
				if (File.Exists(resource))
				{
					// Add file to download table
					list.Add(resource);
				}
			}

			// Get the root folder path
			root = GetRootFolder(model);
			if (root.Length == 0)
            {
				return (null, list);
            }

			// Opens the file
			using (FileStream stream = File.OpenRead(model))
			using (BinaryReader reader = new BinaryReader(stream))
			{
				// Find the total materials amount
				stream.Seek(204, SeekOrigin.Begin);
				numMat = reader.ReadInt32();
				stream.Seek(0, SeekOrigin.End);

				do /// Reads a single binary char
				{
					stream.Seek(-2, SeekOrigin.Current);
					ch = reader.ReadByte();
				}
				while (ch == 0);

				// Shift the cursor a bit
				stream.Seek(-1, SeekOrigin.Current);

				do /// Reads a single binary char
				{
					stream.Seek(-2, SeekOrigin.Current);
					ch = reader.ReadByte();
				}
				while (ch != 0);

				// Reads a UTF8 or ANSI string from a file
				long posIndex = stream.Position;
				material = ReadNullTerminatedString(reader);
				stream.Seek(posIndex, SeekOrigin.Begin);
				stream.Seek(-1, SeekOrigin.Current);

				// Initialize a material list array
				ArrayList matList = new ArrayList();
				ArrayList texList = new ArrayList();

				// Reverse loop throught the binary
				while (stream.Position > 1 && matList.Count < numMat)
				{
					do /// Reads a single binary char
					{
						stream.Seek(-2, SeekOrigin.Current);
						ch = reader.ReadByte();
					}
					while (ch != 0);

					// Reads a UTF8 or ANSI string from a file
					posIndex = stream.Position;
					path = ReadNullTerminatedString(reader);
					stream.Seek(posIndex, SeekOrigin.Begin);
					stream.Seek(-1, SeekOrigin.Current);

					// Validate size
					if (path.Length == 0)
					{
						continue;
					}

					// Finds the first occurrence of a character in a string
					format = path.LastIndexOf('\\');

					// Validate no format
					if (format != -1)
					{
						// Format full path to directory
						path = root + "\\materials\\" + path;

						// Search files in the directory
						foreach (string file in Directory.GetFiles(path))
						{
							// Validate format
							if (Path.GetExtension(file) == ".vmt")
							{
								// Format full path to file
								string full = path + file;

								// Validate unique material
								if (matList.IndexOf(full) != -1)
								{
									continue;
								}

								// Precache model textures
								if (ReadTextures(root, full, texList))
                                {
									// Push data into array
									matList.Add(full);
								}
							}
						}
					}
					else
					{
						// Format full path to file
						path = root + "\\materials\\" + material + path + ".vmt";

						// Validate unique key
						if (matList.IndexOf(path) != -1)
						{
							continue;
						}

						// Precache model textures
						if (ReadTextures(root, path, texList))
                        {
							// Push data into array
							matList.Add(path);
						}
					}
				}

				// Merge data to the main list
				list.AddRange(matList);
				list.AddRange(texList);
			}

			// Close file
			return (root, list);
		}

		/**
		 * @brief Reads the current material and adds them to the its textures list.
		 *
		 * @param root      The root path.
		 * @param texture   The texture path.
		 * @param texList   The output array.
		 * @return          True if was found, false otherwise.
		 **/
		static bool ReadTextures(string root, string texture, ArrayList texList)
		{
			// If doesn't exist stop
			if (!File.Exists(texture))
			{
				return false;
			}

			// Initialize variables
			string[] types = new string[] { "$baseTexture", "$bumpmap", "$lightwarptexture", "$REFRACTTINTtexture" }; bool[] found = new bool[4]; int shift;

			// Opens the file
			string[] lines = File.ReadAllLines(texture);

			// Read lines in the file
			foreach (string str in lines)
			{
				// Cut out comments at the end of a line
				string line = StripComments(str);
				
				// i = texture type
				for (int x = 0; x < 4; x++)
				{
					// Avoid the reoccurrence 
					if (found[x])
					{
						continue;
					}

					// Validate type
					if ((shift = line.IndexOf(types[x], StringComparison.CurrentCultureIgnoreCase)) != -1)
					{
						// Sets on success
						found[x] = true;

						// Shift the type away
						shift += types[x].Length + 1;

						// Trim string
						line = line.Substring(shift).Trim().Trim('"');

						// Format full path to file
						line = root + "\\materials\\" + line + ".vtf";

						// Validate material
						if (File.Exists(line))
						{
							// Add file to download table
							texList.Add(line);
						}
					}
				}
			}

			// Close file
			return true;
		}

		/**
		 * @brief Extract the root path form a absolute path.
		 *
		 * @param line      The given path.
		 * @return          The root path.
		 **/
		static string GetRootFolder(string path)
		{
			// Initialize variables
			string[] types = new string[] { "\\models\\", "\\models/", "/models\\", "/models/" };
			ArrayList list = new ArrayList(); int index;

			// Checked substrings in the path
			foreach (string type in types)
            {
				// Validate type
				if ((index = path.IndexOf(type)) != -1)
                {
					// Add found index
					list.Add(index);
				}
			}

			// Extract the root
			return path.Substring(0, list.Count != 0 ? list.Cast<int>().Min() : 0);
		}

		/**
		 * @brief Remove the any comments for a given line.
		 *
		 * @param line      The given line.
		 * @return          The output string.
		 **/
		static string StripComments(string line)
		{
			var regex = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
			return Regex.Replace(line, regex, "$1");
		}

		/**
		 * @brief Reads the null terminated string.
		 *
		 * @param stream    The given stream.
		 * @return          The output string.
		 **/
		static string ReadNullTerminatedString(BinaryReader stream)
		{
			string str = ""; char ch;
			while ((int)(ch = stream.ReadChar()) != 0)
			{
				str += ch;
			}
			return str;
		}
	}
}