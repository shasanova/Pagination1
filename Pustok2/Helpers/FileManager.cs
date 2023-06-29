﻿using System;
using Pustok2.Entities;

namespace Pustok2.Helpers
{
	public static class FileManager
	{
		public static string Save(IFormFile file, string rootPath, string folder)
        {
            string newFileName = Guid.NewGuid().ToString() + (file.FileName.Length <= 64 ? file.FileName : file.FileName.Substring(file.FileName.Length - 64));
            string path = Path.Combine(rootPath, folder, newFileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return newFileName;
        }
    }
}

