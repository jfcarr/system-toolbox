namespace SystemToolbox.Commands
{
    public static class Disk
    {
        public static void ShowFreeSpace(bool showAll, bool verbose)
        {
            try
            {
                if (showAll)
                {
                    foreach (DriveInfo drive in DriveInfo.GetDrives())
                    {
                        try
                        {
                            if (drive.IsReady)
                                if (drive.TotalFreeSpace > 0)
                                    DisplayAvailable(drive.Name, drive.AvailableFreeSpace);
                        }
                        catch (Exception ex)
                        {
                            if (verbose)
                                Console.WriteLine(ex.Message);
                        }
                    }
                }
                else
                {
                    var driveInfo = new DriveInfo(".");
                    DisplayAvailable(driveInfo.Name, driveInfo.AvailableFreeSpace);
                }
            }
            catch (System.Exception ex)
            {
                if (verbose)
                    Console.WriteLine(ex.Message);
            }
        }

        private static void DisplayAvailable(string driveName, long availableBytes)
        {
            Console.WriteLine($"{driveName} {BytesToMB(availableBytes)} MB ({BytesToGB(availableBytes)} GB) available");
        }

        private static long BytesToMB(long bytesValue)
        {
            return bytesValue / 1024 / 1024;
        }

        private static long BytesToGB(long bytesValue)
        {
            return BytesToMB(bytesValue) / 1024;
        }
    }
}