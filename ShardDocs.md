# Shard Documentation

## How to make a shell?

### 1 : Add a Shell and CommandRegister field

Your code should look like this :

```
using SipaaKernelV3.Shard;

public class Kernel : Sys.Kernel
{
    public static Shell sh;
    public static CommandRegister cr;
```

### 2 : Load Shell

Add this code in BeforeRun method of your kernel :

```
protected override void BeforeRun()
{
    cr = new CommandHandler();
    sh = new Shell(cr);
}
``` 

### 3 : Get and Run command

To make this, call `Shell.GetAndRunCommannd();`

This is all for making a shell!

## How to make a command?

### 1 : Create a class than inherits `SipaaKernelV3.Shard.Command`

### 2 : Add an initializer

This is very important because the Command abstract class needs your command name, description and usages

Here is a sample :

```
public HelloWorldCommand() : base("helloworld", "Hello World Command", new string[] { "helloworld [arg 1]","helloworld" })
{ 

}
```

### 3 : Add the Execute abstract method

To make this, Visual Studio makes you an error, Click on Solutions and add the Execute method, and add your all your code and a result in Execute method.

### 4 : Register Command

Super, your command is made but not registered...

To register, go to your CommandRegister and call `CommandRegister.AppendCommand(Command)` with an instance of your command

Sample : `cr.AppendCommand(new HelloWorldCommand());`

Now, run and try your command!