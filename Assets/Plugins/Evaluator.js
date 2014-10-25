
var a:int = 3;
var b:int = 2;
var test:String;
var variables = new Array();

function Start () {

}

function Update () {
    var code:String = "test = 'test variable';";

    eval(code);
    code = "print (test)";

    eval(code);
    code = "test = 'new variable';";

    eval(code);
    code = "print (test)";

    eval(code);

}

function Eval(messageContext) {
    //code = "print ('invoked from update: '+ (a * b) )";
    
    messageContext.Output = eval(messageContext.Input);
}   