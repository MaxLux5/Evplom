using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Diplom;

partial class TextFile : ObservableObject
{
    [JsonIgnore] private readonly string _path;
    [ObservableProperty] private ObservableCollection<Header> _headers;
    [ObservableProperty] private string _text;
    private TextFile(string path)
    {
        _path = path;
        _headers = new();
        _text = string.Empty;
    }

    public void CreateHeader(int line, string text)
    {
        Headers.Add(new Header(text, line));
    }
    public void DeleteHeader(Header selectedHeader)
    {
        if (Headers.Contains(selectedHeader))
            Headers.Remove(selectedHeader);
    }

    public void Save()
    {
        using var fs = File.Create(_path);
        using var sw = new StreamWriter(fs);
        var content = JsonSerializer.Serialize(this)!;
        sw.Write(content);
    }

    public static TextFile Open(string path)
    {
        using var fs = File.OpenRead(path);
        using var sr = new StreamReader(fs);
        var str = sr.ReadToEnd();
        var file = new TextFile(path);
        try
        {
            var content = JsonSerializer.Deserialize<TextFileDto>(str)!;
            file.Headers = [.. content.Headers];
            file.Text = content.Text;
            return file;
        }
        catch
        {
            fs.Dispose();
            sr.Dispose();
            return Create(path);
        }
    }
    public static TextFile Create(string path)
    {
        using var fs = File.Create(path);
        return new TextFile(path);
    }
}
