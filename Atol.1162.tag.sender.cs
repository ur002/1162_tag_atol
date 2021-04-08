// формирование тэга 1162, кода маркировки, для отправки в ОФД. 
 private static  char[] CHARS_TABLES = "0123456789ABCDEF".ToCharArray();
 private static  byte[] BYTES = new byte[128];

string group = "444D";
string gtin  = "02900000475830";
string serial = "MdEfx:Xp6YFd7";

public static byte[] parseHex(String aHexString)
{
    char[] src = aHexString.Replace("\n", "").Replace(" ", "").ToUpper().Replace("0X", "").Replace("-", "").ToCharArray();
    byte[] dst = new byte[src.Length / 2];
    int si = 0;

    for (int di = 0; di < dst.Length; ++di)
    {
        byte high = BYTES[src[si++] & 127];
        byte low = BYTES[src[si++] & 127];
        dst[di] = (byte)((high << 4) + (low & 255));
    }

    return dst;
}


public static String toHexString(byte[] aBytes, int aOffset, int aLength)
{
    if (aLength == -1)
        aLength = aBytes.Length;
    char[] dst = new char[aLength * 2];
    int di = 0;

    for (int si = aOffset; si < aOffset + aLength; ++si)
    {
        byte b = aBytes[si];
        dst[di++] = CHARS_TABLES[(b & 240) >> 4];
        dst[di++] = CHARS_TABLES[b & 15];
    }

    return new String(dst);
}

private void SendCode()
{
for (int i = 0; i < 10; ++i)
        {
            BYTES[48 + i] = (byte)i;
            BYTES[65 + i] = (byte)(10 + i);
            BYTES[97 + i] = (byte)(10 + i);
        }

string str = group.ToUpper() +
             Convert.ToString(long.Parse(gtin), 16).ToUpper().PadLeft(12, '0') +
             toHexString(Encoding.ASCII.GetBytes(serial), 0, -1);
var code4send = parseHex(str);    
           
Fptr.setParam(Fptr.LIBFPTR_PARAM_RECEIPT_TYPE, 1) 
Fptr.setParam(1055, 0) 
Fptr.openReceipt()   

Fptr.setParam(Fptr.LIBFPTR_PARAM_COMMODITY_NAME, 'Товар') 
Fptr.setParam(Fptr.LIBFPTR_PARAM_PRICE, 100) 
Fptr.setParam(Fptr.LIBFPTR_PARAM_QUANTITY, 1)
Fptr.setParam(Fptr.LIBFPTR_PARAM_POSITION_SUM, 100) 
Fptr.setParam(Fptr.LIBFPTR_PARAM_TAX_TYPE, 7) 
Fptr.setParam(1212, 1) 
Fptr.setParam(1214, 1) 
Fptr.setParam(1162, code4send) 
Fptr.registration()   

Fptr.setParam(Fptr.LIBFPTR_PARAM_PAYMENT_TYPE, 0) 
Fptr.closeReceipt()  

}
 
                         
                         
