using System.Runtime.InteropServices;

namespace PathCreate
{
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 1024)]
	public struct HEADER
	{
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
		public string Signature;

		[FieldOffset(256)]
		public uint Version;

		[FieldOffset(260)]
		public uint FileCounts;

		[FieldOffset(264)]
		public uint TableOffset;

		[FieldOffset(268)]
		public uint Unknown;
	}
}
