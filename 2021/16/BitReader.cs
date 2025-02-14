using System.Collections;

namespace _16
{
	public class BitReader
	{
		private readonly BitArray bits;
		private int ptr;

		public BitReader(BitArray bits)
		{
			this.bits = bits;
		}

		public bool Any()
		{
			return ptr < bits.Length;
		}

		public BitReader GetBitReader(int bitCount)
		{
			var bitArray = new BitArray(bitCount);
			for (var i = 0; i < bitCount; i++)
			{
				bitArray.Set(i, bits[ptr++]);
			}
			return new BitReader(bitArray);
		}

		public int ReadInt(int bitCount)
		{
			var res = 0;
			for (var i = 0; i < bitCount; i++)
			{
				res = res * 2 + (bits[ptr++] ? 1 : 0);
			}
			return res;
		}
	}

	record Packet(int version, int type, long payload, Packet[] packets);
}
