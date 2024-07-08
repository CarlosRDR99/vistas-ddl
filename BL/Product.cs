using DLEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace BL
{
    public class Product
    {
        //public static ML.Result GetAll() {
        //ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (NorthwindEntities context = new NorthwindEntities())
        //        {
        //            var query = context.ProductGetAll().ToList();
        //            if (query.Count>0)
        //            {
        //                result.Objects = new List<object>();
        //                foreach (var fila in query)
        //                {
        //                    ML.Product product = new ML.Product();
        //                    product.ProductID = fila.ProductID;
        //                    product.ProductName = fila.ProductName;
        //                    product.QuantityPerUnit = fila.QuantityPerUnit;
        //                    product.UnitPrice = fila.UnitPrice.Value;
        //                    product.UnitsInStock = fila.UnitsInStock.Value;
        //                    product.UnitsOnOrder = fila.UnitsOnOrder.Value;
        //                    product.ReorderLevel = fila.ReorderLevel.Value;
        //                    product.Discontinued = fila.Discontinued;
        //                    result.Objects.Add(product);
        //                }
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct=false;
        //            }
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        result.Correct=false;
        //        result.ErrorMessage = exp.Message;
        //        result.Exc = exp;
        //    }
        //    return result;  
        //}
        public static ML.Result GetAllSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (NorthwindEntities context = new NorthwindEntities())
                {
                    var query = context.ProductGetAll().ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var fila in query)
                        {
                            result.Objects.Add(fila);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception exp)
            {
                result.Correct = false;
                result.ErrorMessage = exp.Message;
                result.Exc = exp;
            }
            return result;
        }
    }
}
