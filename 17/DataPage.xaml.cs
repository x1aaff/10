using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace _17
{
    /// <summary>
    /// Логика взаимодействия для DataPage.xaml
    /// </summary>
    public partial class DataPage : Page
    {
        OleDbDataAdapter adapterOleDb = new OleDbDataAdapter();
        OleDbDataAdapter adapterClientele = new OleDbDataAdapter();
        DataTable dataTableOleDbClientele = new DataTable();
        DataTable dataTableOleDb = new DataTable();
        DataRowView dataRowView;

        SqlDataAdapter adapterSql = new SqlDataAdapter();
        DataTable dataTableSql = new DataTable();

        public DataPage()
        {
            InitializeComponent();

            TBOleString.Text = Connections.connectionString2;
            TBSqlString.Text = Connections.connectionString1;

            Connections.Activate();

            Connections.sqlConnection.StateChange += (s, e) => TBSqlStatus.Text = (s as SqlConnection).State.ToString();
            Connections.oleDbConnection.StateChange += (s, e) => TBOleStatus.Text = (s as OleDbConnection).State.ToString();

            Connections.Connect();

            #region команды
            //select
            var sql = @"SELECT * FROM Clients";
            adapterSql.SelectCommand = new SqlCommand(sql, Connections.sqlConnection);
            sql = @"SELECT * FROM Products";
            adapterOleDb.SelectCommand = new OleDbCommand(sql, Connections.oleDbConnection);
            sql = @"SELECT * FROM Products WHERE Email = @Email";
            adapterClientele.SelectCommand = new OleDbCommand(sql, Connections.oleDbConnection);
            adapterClientele.SelectCommand.Parameters.Add("@Email", OleDbType.VarChar, 20, "Email");

            //insert
            sql = $@"
                INSERT INTO Clients (LastName, FirstName, PatronymicName, PhoneNumber, Email)
                VALUES (@LastName, @FirstName, @PatronymicName, @PhoneNumber, @Email);
                SET @Id = @@IDENTITY;
                ";
            adapterSql.InsertCommand = new SqlCommand(sql, Connections.sqlConnection);

            adapterSql.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 4, "Id").Direction =
                ParameterDirection.Output;
            adapterSql.InsertCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50, "LastName");
            adapterSql.InsertCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
            adapterSql.InsertCommand.Parameters.Add("@PatronymicName", SqlDbType.NVarChar, 50, "PatronymicName");
            adapterSql.InsertCommand.Parameters.Add("@PhoneNumber", SqlDbType.Int, 4, "PhoneNumber");
            adapterSql.InsertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");

            sql = $@"
                INSERT INTO Products (Email, ProductCode, ProductName)
                VALUES (@Email, @ProductCode, @ProductName);";
            adapterOleDb.InsertCommand = new OleDbCommand(sql, Connections.oleDbConnection);

            adapterOleDb.InsertCommand.Parameters.Add("@Email", OleDbType.VarChar, 20, "Email");
            adapterOleDb.InsertCommand.Parameters.Add("@ProductCode", OleDbType.Integer, 4, "ProductCode");
            adapterOleDb.InsertCommand.Parameters.Add("@ProductName", OleDbType.LongVarChar, 50, "ProductName");
            
            //update
            sql = @"UPDATE Clients SET 
                           LastName = @LastName,
                           FirstName = @FirstName,
                           PatronymicName = @PatronymicName,
                           PhoneNumber = @PhoneNumber,
                           Email = @Email
                    WHERE Id = @Id";
            adapterSql.UpdateCommand = new SqlCommand(sql, Connections.sqlConnection);

            adapterSql.UpdateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id").SourceVersion = DataRowVersion.Original;
            adapterSql.UpdateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 50, "LastName");
            adapterSql.UpdateCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
            adapterSql.UpdateCommand.Parameters.Add("@PatronymicName", SqlDbType.NVarChar, 50, "PatronymicName");
            adapterSql.UpdateCommand.Parameters.Add("@PhoneNumber", SqlDbType.Int, 4, "PhoneNumber");
            adapterSql.UpdateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");

            //delete
            sql = "DELETE FROM Clients WHERE Id = @Id";
            adapterSql.DeleteCommand = new SqlCommand(sql, Connections.sqlConnection);
            adapterSql.DeleteCommand.Parameters.Add("@Id", SqlDbType.Int, 4, "Id");

            sql = "DELETE FROM Products WHERE Id = @Id";
            adapterOleDb.DeleteCommand = new OleDbCommand(sql, Connections.oleDbConnection);
            adapterOleDb.DeleteCommand.Parameters.Add("@Id", OleDbType.Integer, 4, "Id");

            #endregion

            adapterOleDb.Fill(dataTableOleDb);
            DGProducts.DataContext = dataTableOleDb.DefaultView;

            adapterSql.Fill(dataTableSql);
            DGClients.DataContext = dataTableSql.DefaultView;
        }

        private void DGClients_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataRowView == null) return;
            dataRowView.EndEdit();
            adapterSql.Update(dataTableSql);
        }

        private void DGClients_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            dataRowView = (DataRowView)DGClients.SelectedItem;
            dataRowView.BeginEdit();
        }

        private void DGProducts_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataRowView == null) return;
            dataRowView.EndEdit();
            adapterOleDb.Update(dataTableOleDb);
        }

        private void DGProducts_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            dataRowView = (DataRowView)DGProducts.SelectedItem;
            dataRowView.BeginEdit();
        }

        private void MenuItemAdd_Click(object sender, RoutedEventArgs e)//клиенты
        {
            DataRow r = dataTableSql.NewRow();
            Window1 add = new Window1(r);
            add.ShowDialog();

            if(add.DialogResult.Value)
            {
                dataTableSql.Rows.Add(r);
                adapterSql.Update(dataTableSql);
            }
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)//клиенты
        {
            dataRowView = (DataRowView)DGClients.SelectedItem;
            dataRowView.Row.Delete();
            adapterSql.Update(dataTableSql);
        }

        private void MenuPAdd_Click(object sender, RoutedEventArgs e)//товары
        {
            DataRow r = dataTableOleDb.NewRow();
            Window2 add = new Window2(r);
            add.ShowDialog();

            if (add.DialogResult.Value)
            {
                dataTableOleDb.Rows.Add(r);
                adapterOleDb.Update(dataTableOleDb);
            }
        }

        private void MenuPDel_Click(object sender, RoutedEventArgs e)//товары
        {
            dataRowView = (DataRowView)DGProducts.SelectedItem;
            dataRowView.Row.Delete();
            adapterOleDb.Update(dataTableOleDb);
        }

        private void DGClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataTableOleDbClientele.Clear();

            dataRowView = (DataRowView)DGClients.SelectedItem;
            string email = dataRowView.Row.ItemArray[dataRowView.Row.ItemArray.Length - 1].ToString();

            adapterClientele.SelectCommand.Parameters[0].Value = email;
            adapterClientele.Fill(dataTableOleDbClientele);
            DGPurchases.DataContext = dataTableOleDbClientele.DefaultView;
        }
    }
}
