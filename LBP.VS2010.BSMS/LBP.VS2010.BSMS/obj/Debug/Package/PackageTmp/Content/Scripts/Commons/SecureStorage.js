

var secureStorage = new SecureStorage(sessionStorage, {
    hash: function hash(key) {
        return key.toString();
    },
    encrypt: function encrypt(data) {     

       
        data = CryptoJS.AES.encrypt(data, 'crypto');

                    data = data.toString();

                    alert('encrypted data ' + data);
                    return data;
       
    },
    decrypt: function decrypt(data) {


        data = CryptoJS.AES.decrypt(data, 'crypto1');

                    data = data.toString(CryptoJS.enc.Utf8);

                    return data;
        

       
    },
    secureEncrypt: function secureEncrypt(data, key) {
        data = CryptoJS.AES.encrypt(data, key);

        data = data.toString();

        return data;

    },
    secureDecrypt: function secureDecrypt(data, key) {        
        data = CryptoJS.AES.decrypt(data, key);

        data = data.toString(CryptoJS.enc.Utf8);

        return data;
    }
});