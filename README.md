# JsonByter
A .NET wrapper for Raid's JsoByte. ([Repo](https://git.gocode.it/RaidAndFade/jsobyte))

### How to get it

You'd want to clone or download this repo as zip and build it from your preferred IDE (Visual studio recommended)

### Documentation (it's rather short)

##### JsoByter:
JsoByter's namespace

##### JsoByteFile:
Object containing all data of your Jbin (use Intellisense pls)

##### GetJson():
Gets Json for your Jbin

##### GetBytes():
Gets bytes from your JsoByteFile

##### FromJson(string) (static):
Converts json string to JsoByteFile object

##### Generate (string filename, string extension, byte[] bytes) (static):
Generates JsoByteFile
