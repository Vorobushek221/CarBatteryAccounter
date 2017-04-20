using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CarBatteryAccounter.Model
{
    public class FileUpdater
    {
        private readonly string carTableName = Properties.Settings.Default.carTableName;

        private readonly string carTablePath = Properties.Settings.Default.carTablePath;

        private readonly string battaryTableName = Properties.Settings.Default.battaryTableName;

        private readonly string battaryTablePath = Properties.Settings.Default.battaryTablePath;

        public void CopyCarTableToLocalDirectory()
        {
            File.Copy(carTablePath + carTableName, Directory.GetCurrentDirectory() + @"\data\db\" + carTableName, true);
        }

        public void CopyBattaryTableToLocalDirectory()
        {
            File.Copy(battaryTablePath + battaryTableName, Directory.GetCurrentDirectory() + @"\data\db\" + battaryTableName, true);
        }
    }
}
