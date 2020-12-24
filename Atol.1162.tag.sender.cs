# формирование тэга 1162, кода маркировки, для отправки в ОФД. 
string group = "444D";
string gtin  = "02900000475830";
string serial = "MdEfx:Xp6YFd7";

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

 
                         
                         
