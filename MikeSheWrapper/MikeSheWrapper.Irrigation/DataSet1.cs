namespace MikeSheWrapper.Irrigation {
    
    
    public partial class DataSet1 {
      private MikeSheWrapper.Irrigation.DataSet1TableAdapters.INTAKETableAdapter intakeTableAdapter1;
      private MikeSheWrapper.Irrigation.DataSet1TableAdapters.TableAdapterManager tableAdapterManager1;
      private MikeSheWrapper.Irrigation.DataSet1TableAdapters.WATLEVELTableAdapter watlevelTableAdapter1;
      private MikeSheWrapper.Irrigation.DataSet1TableAdapters.BOREHOLETableAdapter boreholeTableAdapter1;
    
      partial class BOREHOLEDataTable
      {
      }

      private void InitializeComponent()
      {
        this.boreholeTableAdapter1 = new MikeSheWrapper.Irrigation.DataSet1TableAdapters.BOREHOLETableAdapter();
        this.intakeTableAdapter1 = new MikeSheWrapper.Irrigation.DataSet1TableAdapters.INTAKETableAdapter();
        this.tableAdapterManager1 = new MikeSheWrapper.Irrigation.DataSet1TableAdapters.TableAdapterManager();
        this.watlevelTableAdapter1 = new MikeSheWrapper.Irrigation.DataSet1TableAdapters.WATLEVELTableAdapter();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // boreholeTableAdapter1
        // 
        this.boreholeTableAdapter1.ClearBeforeFill = true;
        // 
        // intakeTableAdapter1
        // 
        this.intakeTableAdapter1.ClearBeforeFill = true;
        // 
        // tableAdapterManager1
        // 
        this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
        this.tableAdapterManager1.BOREHOLETableAdapter = this.boreholeTableAdapter1;
        this.tableAdapterManager1.INTAKETableAdapter = this.intakeTableAdapter1;
        this.tableAdapterManager1.UpdateOrder = MikeSheWrapper.Irrigation.DataSet1TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
        this.tableAdapterManager1.WATLEVELTableAdapter = this.watlevelTableAdapter1;
        // 
        // watlevelTableAdapter1
        // 
        this.watlevelTableAdapter1.ClearBeforeFill = true;
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

      }
    }
}
