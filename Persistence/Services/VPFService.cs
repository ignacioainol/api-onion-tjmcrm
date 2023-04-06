using Application.Features.Items.Queries.GetItemsByOrderIdQuery;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Views;
using System.Data.OleDb;
using System.Diagnostics;

namespace Persistence.Services
{
    public class VPFService : IVPFService
    {
        string connectionString = @"Provider=vfpoledb;Data Source=C:\VFP\ecom;Collating Sequence=machine;";

        public async Task<Response<Order>> GetOrderById(GetOrderByIdQuery request)
        {
            Order order = new Order();
            OleDbConnection DB = new();
            try
            {
                DB.ConnectionString = connectionString;
                DB.Open();

                OleDbCommand cmd = new($@"SELECT ORDHDR.ORDNBR, ORDHDR.XFRLOC, ORDHDR.ORDDATE, ORDHDR.COFFST, ORDHDR.SLSPROF,ORDHDR.CUST, 
                                        CUST.LNAME, CUST.FNAME, CUST.MINIT, ORDSTA.DESCR
                                        FROM ('c:\VFP\latest\ORDHDR.dbf')
                                        JOIN ('c:\VFP\latest\CUST.dbf') CUST ON ORDHDR.CUST = CUST.CUST
                                        JOIN ('c:\VFP\ecom\ORDSTA.dbf') ON ORDHDR.STATUS = ORDSTA.ORDSTA
                                        WHERE ORDHDR.ORDNBR = '{request.OrderId}'
                                        ORDER BY ORDHDR.ADDDATE", DB);

                //WHERE ORDHDR.XFRLOC = '{request.OrderLocation}' AND ORDHDR.ORDNBR = '{request.OrderId}'

                var reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    order.ORDNBR = (string)reader["ORDNBR"];
                    order.ORGLOC = (string)reader["XFRLOC"];
                    order.ORDDATE = (DateTime)reader["ORDDATE"];
                    order.COFFST = (string)reader["COFFST"];
                    order.SLSPROF = (string)reader["SLSPROF"];
                    order.CUST = (string)reader["CUST"];
                    order.LNAME = (string)reader["LNAME"];
                    order.FNAME = (string)reader["FNAME"];
                    order.MINIT = (string)reader["MINIT"];
                    order.DESCR = (string)reader["DESCR"];
                }
                DB.Close();

                var data = new Response<Order>(order);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"mensaje servicio 1 :{ex.Message}");
            }

        }

        public async Task<Response<List<Item>>> GetItemsByOrderId(GetItemsByOrderIdQuery request)
        {
            List<Item> orderItems = new List<Item>();
            OleDbConnection DB = new();

            try
            {
                DB.ConnectionString = connectionString;
                DB.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"mensaje servicio 1 :{ex.Message}");
            }

            OleDbCommand cmd = new($@"select iif(!empty(z09rcvd), .t., .f.) as rcv, ordline, mfgloc, prdtyp, linqty, sku, TRIM(co.descr) + ' '+ TRIM(ca.descr) as description, TRANSFORM('') AS sizrmd, z09rcvd as rcvdate, s.descr as status, o.chgdate
                                        from ('c:\vfp\DBFS\ordcusu.dbf') o
                                        left join ('c:\VFP\DBFS\color.dbf') co on o.color = co.color and co.xfrloc = 'EAM'
                                        left join ('c:\VFP\DBFS\categ.dbf')ca on o.categ = ca.categ and ca.xfrloc = 'EAM'
                                        left join ('c:\VFP\DBFS\status.dbf') s on o.status = s.status
                                        where o.xfrloc = '{request.OrderLocation}' and o.ordnbr = '{request.OrderId}'
                                        union
                                        select iif(!empty(z09rcvd), .t., .f.) as rcv, ordline, mfgloc, prdtyp, linqty, sku, TRIM(co.descr) + ' '+ TRIM(ca.descr) as description, TRANSFORM('') AS sizrmd, z09rcvd as rcvdate, s.descr as status, o.chgdate
                                        from ('c:\VFP\DBFS\ORDCUSH.dbf') o
                                        left join ('c:\VFP\DBFS\color.dbf') co on o.color = co.color and co.xfrloc = 'ISM'
                                        left join ('c:\VFP\DBFS\categ.dbf') ca on o.categ = ca.categ and ca.xfrloc = 'ISM' 
                                        left join ('c:\VFP\DBFS\status.dbf') s on o.status = s.status
                                        where o.xfrloc = '{request.OrderLocation}' and ordnbr = '{request.OrderId}'
                                        union
                                        select iif(!empty(z09rcvd), .t., .f.) as rcv, ordline, mfgloc, prdtyp, linqty, skurmd as sku, o.descr as description, o.sizrmd , z09rcvd as rcvdate, s.descr as status, o.chgdate
                                        from ('c:\VFP\DBFS\ordrmd.dbf') o
                                        left join ('c:\VFP\DBFS\dimrmd.dbf') d on o.dimrmd = d.dimrmd
                                        left join ('c:\VFP\DBFS\status.dbf') s on o.status = s.status
                                        where o.xfrloc = '{request.OrderLocation}' and ordnbr = '{request.OrderId}'
                                        order BY ordline, sizrmd", DB);

            try
            {
                var reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    Item orderItem = new Item();
                    orderItem.Rcv = (string)reader["Rcv"].ToString();
                    orderItem.Ordline = (string)reader["ordline"];
                    orderItem.Mnfr = (string)reader["mfgloc"];
                    orderItem.Prdtyp = (string)reader["prdtyp"];
                    orderItem.Linqty = (Decimal)reader["Linqty"];
                    orderItem.Sku = (string)reader["Sku"];
                    orderItem.Description = (string)reader["Description"];
                    orderItem.Sizrmd = (string)reader["Sizrmd"];
                    orderItem.Rcvdate = (DateTime)reader["Rcvdate"];
                    //orderItem.Status = (string)reader["Status"];
                    orderItem.Chgdate = (DateTime)reader["Chgdate"];
                    orderItems.Add(orderItem);
                }
                DB.Close();

                return new Response<List<Item>>(orderItems);

            }
            catch (Exception ex1)
            {

                throw new Exception($"mensaje servicio 2 :{ex1.Message}");
            }
        }

        public async Task<PageResponse<List<Order>>> GetOrders(GetAllOrdersQuery request)
        {
            List<Order> orders = new List<Order>();
            OleDbConnection DB = new();
            try
            {
                DB.ConnectionString = connectionString;
                DB.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"mensaje servicio 1 :{ex.Message}");
            }

            string query = $@"SELECT ORDHDR.ORDNBR, ORDHDR.XFRLOC, ORDHDR.ORDDATE, ORDHDR.COFFST, ORDHDR.SLSPROF,
                              ORDHDR.CUST, CUST.LNAME, CUST.FNAME, CUST.MINIT, ORDSTA.DESCR
                              FROM ('c:\VFP\latest\ORDHDR.dbf')
                              JOIN ('c:\VFP\latest\CUST.dbf') CUST ON ORDHDR.CUST = CUST.CUST
                              JOIN ORDSTA ON ORDHDR.STATUS = ORDSTA.ORDSTA
                              WHERE ORDHDR.XFRLOC = '{request.OrderLocation}'";

            if (request.SearchOrder != "" && request.SearchOrder != null) {
                query += $" AND (ORDHDR.ORDNBR LIKE '{request.SearchOrder}%' OR ORDHDR.COFFST LIKE '{request.SearchOrder}%')";
            }

            if (request.SalesPerson != "" && request.SalesPerson != null)
            {
                query += $" AND ORDHDR.SLSPROF = '{request.SalesPerson}'";
            }

            if (request.SelectStatus != "" && request.SelectStatus != null)
            {
                query += $" AND ORDSTA.DESCR = '{request.SelectStatus}'";
            }

            var orderBy = "";

            switch (request.SelectSequence)
            {
                case "clientName":
                    orderBy += orderBy == "" ? " ORDER BY CUST.LNAME ASC" :  ", CUST.LNAME ASC";
                    break;
                case "clientNumber":
                    orderBy += orderBy == "" ? " ORDER BY ORDHDR.CUST ASC" : ", ORDHDR.CUST ASC";
                    //query += " ORDER BY ORDHDR.CUST ASC";
                    break;
                default:
                    orderBy += orderBy == "" ? " ORDER BY ORDHDR.ADDDATE" : " ,ORDHDR.ADDDATE";
                     //query += " ORDER BY ORDHDR.ADDDATE";
                    break;
            }

            query += orderBy;

            OleDbCommand cmd = new(query, DB);

            try
            {
                var reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    Order data = new Order();
                    data.ORDNBR = (string)reader["ORDNBR"];
                    data.ORGLOC = (string)reader["XFRLOC"];
                    data.ORDDATE = (DateTime)reader["ORDDATE"];
                    data.COFFST = (string)reader["COFFST"];
                    data.SLSPROF = (string)reader["SLSPROF"];
                    data.CUST = (string)reader["CUST"];
                    data.LNAME = (string)reader["LNAME"];
                    data.FNAME = (string)reader["FNAME"];
                    data.MINIT = (string)reader["MINIT"];
                    data.DESCR = (string)reader["DESCR"];
                    orders.Add(data);
                }
                DB.Close();
            }
            catch (Exception ex3)
            {
                throw new Exception($"mensaje servicio 2 :{ex3.Message}");
            }

            request.RecordsTotal = orders.Count();
            request.RecordsFiltered = orders.Count();

            return new PageResponse<List<Order>>(orders, request.RecordsTotal, request.RecordsFiltered);
        }

        public async Task<Response<List<Item>>> GetItemByOrderId(GetItemsByOrderIdQuery request)
        {
            throw new NotImplementedException();
        }
    }
}