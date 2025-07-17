using System;

namespace UnityEngine.Purchasing.Security
{
	public class AppleTangle
	{
		private static byte[] data = Convert.FromBase64String("BSQzNmAxPyY/dEVFWVAVfFtWGwS6RrRV8y5uPBqnh81xfcVVDasgwGySMDxJInVjJCtB5oK+Fg5yluBaRVlQFXZQR0FcU1xWVEFcWlsVdEAIE1IVvwZfwji3+uvelhrMZl9uURVUW1EVVlBHQVxTXFZUQVxaWxVFG3WTwnJ4Sj1rBSozNmAoFjEtBSOLwUau2+dROv5MegHtlwvMTcpe/SMFITM2YDE2Jjh0RUVZUBVnWlpBW1EVVlpbUVxBXFpbRhVaUxVARlAy2UgMtr5mFeYN8YSKr3o/XsoeyUwVVEZGQFhQRhVUVlZQRUFUW1ZQviy868x+WcAynhcFN90tC81lPOYzBTozNmAoJjQ0yjEwBTY0NMoFKEdUVkFcVlAVRkFUQVBYUFtBRhsFUro9gRXC/pkZFVpFgwo0BbmCdvofs32zwjg0NDAwNQVXBD4FPDM2YBkVVlBHQVxTXFZUQVAVRVpZXFZMtSEe5VxyoUM8y8FeuBt1k8JyeEo4Mzwfs32zwjg0NDAwNTa3NDQ1aRHX3uSCReo6cNQS/8RYTdjSgCIiAAcEAQUGA28iOAYABQcFDAcEAQV87UOqBiFQlEKh/Bg3NjQ1NJa3NPVWBkLCDzIZY97vOhQ7749GLHqAEwURMzZgMT4mKHRFRVlQFXZQR0EzNmAoOzEjMSEe5VxyoUM8y8FeuFdZUBVGQVRbUVRHURVBUEdYRhVUKrC2sC6sCHICx5yudbsZ4YSlJ+3sA0r0smDskqyMB3fO7eBEq0uUZ6CrTzmRcr5u4SMCBv7xOnj7IVzkSnSdrczk/1OpEV4k5ZaO0S4f9io9awW3NCQzNmAoFTG3ND0FtzQxBVEAFiB+IGwohqHCw6mr+mWP9G1lA6x5GE2C2Lmu6cZCrsdD50IFevSAD5jBOjs1pz6EFCMbQeAJOO5XI0FdWkdcQUwEIwUhMzZgMTYmOHRF/CxHwGg74EpqrscQNo9gunhoOMRwSyp5XmWjdLzxQVc+JbZ0sga/tFxTXFZUQVxaWxV0QEFdWkdcQUwEMTMmN2BmBCYFJDM2YDE/Jj90RUVZUBV8W1YbBBMFETM2YDE+Jih0RUFcU1xWVEFQFVdMFVRbTBVFVEdBZ1BZXFRbVlAVWlsVQV1cRhVWUEeEBW3ZbzEHuV2GuijrUEbKUmtQiZ3pSxcA/xDg7DrjXuGXERYkwpSZBbcxjgW3NpaVNjc0Nzc0NwU4MzwVdnQFtzQXBTgzPB+zfbPCODQ0NBoFtPYzPR4zNDAwMjc3BbSDL7SGFVpTFUFdUBVBXVBbFVRFRVlcVlQqpO4rcmXeMNhrTLEY3gOXYnlg2QYDbwVXBD4FPDM2YDEzJjdgZgQmOqgIxh58HS/9y/uAjDvsaynj/ghFWVAVZ1paQRV2dAUrIjgFAwUBB56WRKdyZmD0mhp0hs3O1kX405Z5TwW3NEMFOzM2YCg6NDTKMTE2NzQwNTa3NDo1Bbc0Pze3NDQ10aScPD0eMzQwMDI3NCMrXUFBRUYPGhpCtzQ1Mzwfs32zwlZRMDQFtMcFHzNCQhtURUVZUBtWWlgaVEVFWVBWVIIuiKZ3EScf8joog3ipa1b9frUiZZ+/4O/RyeU8MgKFQEAU");

		private static int[] order = new int[61]
		{
			9,
			55,
			52,
			4,
			56,
			45,
			31,
			50,
			12,
			43,
			51,
			40,
			16,
			29,
			46,
			21,
			55,
			44,
			32,
			47,
			53,
			50,
			27,
			57,
			51,
			30,
			42,
			49,
			58,
			32,
			44,
			33,
			53,
			48,
			57,
			45,
			47,
			44,
			40,
			52,
			56,
			52,
			56,
			56,
			54,
			56,
			53,
			54,
			52,
			50,
			55,
			55,
			52,
			55,
			58,
			55,
			56,
			57,
			58,
			59,
			60
		};

		private static int key = 53;

		public static readonly bool IsPopulated = true;

		public static byte[] Data()
		{
			if (!IsPopulated)
			{
				return null;
			}
			return null;// Obfuscator.DeObfuscate(data, order, key);
		}
	}
}
