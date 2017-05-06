var  CHARS  =  '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'.split('');  
var baseUtils = { 
    NewUUID: function()  {      
        var  chars  =  CHARS,
             uuid  =  new  Array(36),
             rnd = 0,
             r;      
        for  (var  i  =  0;  i  <  36;  i++)  {        
            if  (i == 8  ||  i == 13  ||   i == 18  ||  i == 23)  {          
                uuid[i]  =  '-';        
            } 
            else  if  (i == 14)  {
                uuid[i]  =  '4';        
            } 
            else  {          
                if  (rnd  <=  0x02) 
                    rnd  =  0x2000000  +  (Math.random() * 0x1000000) | 0;          
                r  =  rnd  &  0xf;          
                rnd  =  rnd  >>  4;          
                uuid[i]  =  chars[(i  ==  19)  ?  (r  &  0x3)  |  0x8  :  r];        
            }      
        }      
        return  uuid.join('');    
    },
    StringFormat: function() {
        if  (arguments.length  ==  0)           
            return  null;       
        var  str  = arguments[0];       
        for  (var  i  =  1;  i  < arguments.length;  i++)  {           
            var  re  =  new  RegExp('\\{'  +  (i - 1 ) +  '\\}',  'gm');           
            str  =  str.replace(re, arguments[i]);       
        }       
        return  str;  
    },
    DateFormat: function(date, fmt) {  
        var  o  =   {       
            "M+" : date.getMonth() + 1,
                              //月份   
                "d+" : date.getDate(),
                                 //日   
                "h+" : date.getHours(),
                                //小时   
                "m+" : date.getMinutes(),
                              //分   
                "s+" : date.getSeconds(),
                              //秒   
                "q+" :  Math.floor((date.getMonth() + 3) / 3),
              //季度   
                "S"  : date.getMilliseconds()              //毫秒          
        };     
        if (/(y+)/.test(fmt))       
            fmt = fmt.replace(RegExp.$1,   (date.getFullYear() + "").substr(4  -  RegExp.$1.length));     
        for (var  k  in  o)       
            if (new  RegExp("(" +  k  + ")").test(fmt))     
                fmt  =  fmt.replace(RegExp.$1,   (RegExp.$1.length == 1)  ?  (o[k])  :  (("00" +  o[k]).substr(("" +  o[k]).length)));     
        return  fmt;
    },
    ArrayContain: function(array, obj) {
        var i = this.length;  
        while (i--) {    
            if (this[i] === obj) {      
                return i;    
            }  
        }  
        return -1;
    },
    ArrayRemove: function(array, index) {
        if (isNaN(dx) || dx > this.length) { return false; }  
        this.splice(dx, 1);
    }
}
export default baseUtils;