var doorPublicKey = 13135480;
var cardPublicKey = 8821721;

var mod = 20201227;
var pow = 0;
var subject = 7L;
var encriptionKey = subject;

while (encriptionKey != doorPublicKey && encriptionKey != cardPublicKey)
{
	encriptionKey = (encriptionKey * subject) % mod;

	pow++;
}

subject = encriptionKey == doorPublicKey
	? cardPublicKey
	: doorPublicKey;

encriptionKey = subject;

while (pow > 0)
{
	encriptionKey = (encriptionKey * subject) % mod;

	pow--;
}

Console.WriteLine(encriptionKey);