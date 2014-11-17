using Infrastructure.Web.Concurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Warehouse.ReadModels.Dtos;

namespace WebAPI.Models
{
    public class InventoryItemDetail : IConcurrencyAware
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CurrentCount { get; set; }
        private string _concurrencyVersion;

        public InventoryItemDetail(Guid id, string name, int currentCount, int version)
        {
            Id = id;
            Name = name;
            CurrentCount = currentCount;
            _concurrencyVersion = version.ToString();
        }

        string IConcurrencyAware.ConcurrencyVersion
        {
            get { return _concurrencyVersion; }
            set { _concurrencyVersion = value; }
        }
    }

    public class InventoryItemListData : InventoryItemListDto
    {
        public InventoryItemListData(Guid id, string name)
            : base(id, name)
        {
        }
    }

    public class InventoryItemListDataCollection : List<InventoryItemListData>, IConcurrencyAware
    {

        public InventoryItemListDataCollection()
        {

        }

        public InventoryItemListDataCollection(IEnumerable<InventoryItemListDto> dtos)
        {
            AddRange(dtos.Select(x => new InventoryItemListData(x.Id, x.Name)));
        }

        string IConcurrencyAware.ConcurrencyVersion
        {
            get
            {
                var builder = new StringBuilder();
                foreach (var item in this)
                {
                    builder.Append(item.Id.ToString());
                    builder.Append(item.Name);
                }
                using (var md5 = new MD5CryptoServiceProvider()) // MD5 is enough no need for SHA1
                {
                    return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(builder.ToString())));
                }
            }
            set
            {
                // nothing!
            }
        }
    }
}