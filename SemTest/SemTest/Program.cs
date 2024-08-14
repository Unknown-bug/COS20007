namespace SemTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            FileSystem fileSystem = new FileSystem();

            File file1 = new File("Document1", "txt", 100);
            File file2 = new File("Image1", "jpg", 2000);

            fileSystem.Add(file1);
            fileSystem.Add(file2);

            Folder folder1 = new Folder("Folder1");
            folder1.Add(new File("FileInFolder1", "doc", 300));

            fileSystem.Add(folder1);

            Folder folder2 = new Folder("Folder2");
            Folder subFolder = new Folder("SubFolder");
            subFolder.Add(new File("FileInSubFolder", "pdf", 400));
            folder2.Add(subFolder);

            fileSystem.Add(folder2);

            Folder emptyFolder = new Folder("EmptyFolder");

            fileSystem.Add(emptyFolder);

            fileSystem.PrintContents();
        }
    }
}
