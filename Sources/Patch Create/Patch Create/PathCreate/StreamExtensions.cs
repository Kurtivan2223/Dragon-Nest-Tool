using System.IO;
using System.Runtime.InteropServices;

namespace PathCreate
{
	public static class StreamExtensions
	{
		public static T ReadStruct<T>(this Stream stream) where T : struct
		{
			int num = Marshal.SizeOf(typeof(T));
			byte[] array = new byte[num];
			stream.Read(array, 0, num);
			GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			T result = (T)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(T));
			gCHandle.Free();
			return result;
		}
	}
}
