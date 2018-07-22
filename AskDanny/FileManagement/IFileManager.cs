namespace FileManagement
{
    public interface IFileManager
    {
        void SaveToFile(string json);
        string LoadFile();
    }
}