using System.IO;
using System.Windows.Forms;

internal class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        string fileContent = string.Empty;
        string newString = string.Empty;

        Console.WriteLine("Choose the TXT file of comma-separated items");

        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.InitialDirectory = "%USERPROFILE%";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "Choose the TXT file of comma-separated items";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
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

        // Turn our new array into a comma-separated string
        for (int i = 0; i < newList.Length; i++)
        {
            newString += newList[i] + ",";
        }

        // Now let's save to a new file
        Stream saveStream;
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        {
            saveFileDialog.InitialDirectory = "%USERPROFILE%";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Save the new TXT file...";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if((saveStream = saveFileDialog.OpenFile()) != null)
                {
                    using (StreamWriter sw = new StreamWriter(saveStream))
                    {
                        sw.WriteLine(newString);
                    }
                    saveStream.Close();
                }

                Console.WriteLine(saveFileDialog.FileName + " saved successfully.");
                Console.WriteLine("Press any key to quit.");
                Console.ReadKey();
            }
        }
    }
}