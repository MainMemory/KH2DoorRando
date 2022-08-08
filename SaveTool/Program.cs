using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveTool
{
	class Program
	{
		static void Main(string[] args)
		{
			using (FileStream fs = File.OpenRead(@"C:\Users\Kate\Documents\KINGDOM HEARTS HD 1.5+2.5 ReMIX\Epic Games Store\8a9e2b04557a4f3e9e58126c12ca4944\BISLPM-66675FM-98.bin"))
			using (BinaryReader br = new BinaryReader(fs))
			using (var tw = File.CreateText("out.txt"))
			{
				fs.Seek(0x3534, SeekOrigin.Begin);
				for (int i = 0; i < 76; i += 4)
					tw.WriteLine("\t" + string.Join(", ", br.ReadBytes(4)));
			}
		}
	}
}
