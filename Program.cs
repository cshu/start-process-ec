using System.Text.Json;
using System.Diagnostics;


string procname = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
Console.WriteLine(procname);
string homedir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
Console.WriteLine(homedir);
string ecSpDir = Path.Combine(homedir, ".everycom", "start-process-ec");

void RunCmd(string[] cmd)
{
    var fileName = cmd[0];
    using Process proc = Process.Start(fileName, new ArraySegment<string>(cmd, 1, cmd.Length - 1));
}
void UseTxt(string cfgfile)
{
    string[] cmd = File.ReadAllLines(cfgfile);
    RunCmd(cmd);
}
void UseJson(string cfgfile)
{
    string cfgstr = File.ReadAllText(cfgfile);
    string[] cmd = JsonSerializer.Deserialize<string[]>(cfgstr)!;
    RunCmd(cmd);
}

string cfg = Path.Combine(homedir, procname + ".start-process-ec.txt");
if (File.Exists(cfg))
{
    UseTxt(cfg);
    return;
}
cfg = Path.Combine(ecSpDir, procname + ".txt");
if (File.Exists(cfg))
{
    UseTxt(cfg);
    return;
}
cfg = Path.Combine(homedir, procname + ".start-process-ec.json");
if (File.Exists(cfg))
{
    UseJson(cfg);
    return;
}
cfg = Path.Combine(ecSpDir, procname + ".json");
if (File.Exists(cfg))
{
    UseJson(cfg);
    return;
}
Console.WriteLine("Cannot find config file.");
Environment.ExitCode = 1;

