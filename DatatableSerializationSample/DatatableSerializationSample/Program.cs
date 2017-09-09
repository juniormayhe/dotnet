using System;
using System.Data;

namespace DatatableSerializationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable table = generateDatatable();
            string json = table.ToJson();
            Console.WriteLine("DATATABLE TO JSON **************************");
            Console.WriteLine(json);

            Console.WriteLine("\n\nJSON TO DATATABLE **************************");
            printDatatable(json.ToDatatable());

            Console.Read();
        }

        private static void printDatatable(DataTable dt)
        {
            int i = 0;
            Console.WriteLine($"{dt.Rows[i]["NegocioID"]}\t" +
                                $"{dt.Rows[i]["Compania"]}\t\t" +
                                $"{dt.Rows[i]["ValorTotalNegocio"]}\t" +
                                $"{dt.Rows[i]["FechaCreacion"]}");
            i++;
            Console.WriteLine($"{dt.Rows[i]["NegocioID"]}\t" +
                                $"{dt.Rows[i]["Compania"]}\t" +
                                $"{dt.Rows[i]["ValorTotalNegocio"]}\t" +
                                $"{dt.Rows[i]["FechaCreacion"]}");
        }

        private static DataTable generateDatatable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("NegocioID", typeof(int));
            table.Columns.Add("Compania", typeof(string));
            table.Columns.Add("ValorTotalNegocio", typeof(long));
            table.Columns.Add("FechaCreacion", typeof(DateTime));

            DataRow newRow = table.NewRow();
            newRow["NegocioID"] = 1;
            newRow["Compania"] = "Exito";
            newRow["ValorTotalNegocio"] = 20000000L;
            newRow["FechaCreacion"] = DateTime.Now;
            table.Rows.Add(newRow);

            DataRow newRow2 = table.NewRow();
            newRow2["NegocioID"] = 2;
            newRow2["Compania"] = "Grupo Exito";
            newRow2["ValorTotalNegocio"] = 30000000L;
            newRow2["FechaCreacion"] = DateTime.Now.AddDays(1);
            table.Rows.Add(newRow2);
            return table;
        }
    }
}
