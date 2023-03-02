internal class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        Console.WriteLine("Choose the TXT file of comma-separated items");

        var fileContent = string.Empty;
        var filePath = string.Empty;

        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.InitialDirectory = "%USERPROFILE%";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Choose the TXT file of comma-separated items";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;

                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
            }
        }
                
        // Our original comma-separated text goes into one array
        string[] contentArray = fileContent.Split(",");
        // And will be sorted into another array in a different order
        string[] newList = new string[contentArray.Length];
        string newString = "";

        for (int i = 0; i < contentArray.Length; i++)
        {
            Random rnd = new Random();
            int place = rnd.Next(contentArray.Length);

            // Here we ensure that no value is overwritten, as that
            // would result in a loss of data
            while (newList[place] != null)
            {
                place = rnd.Next(contentArray.Length);
            }

            newList[place] = contentArray[i];
        }

        for (int i = 0; i < newList.Length; i++)
        {
            newString += newList[i] + ",";
        }

        // Ideally, and I should do this if I need this tool more
        // than just this one time, I would make a new TXT file and
        // write this in there. But, since I only need to do this
        // this one time, I'll just copy the output from the VS
        // debug console.
        Console.WriteLine(newString);
    }
}