using System.Runtime.InteropServices;

namespace PathCreate
{
	public struct FILELIST
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public byte[] NameFile;

		public uint SizeCompression;

		public uint SizeUncopresion;

		public uint hSizeCompression;

		public uint StartCompression;

		public uint Level;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
		public byte[] unknown;
	}
}
