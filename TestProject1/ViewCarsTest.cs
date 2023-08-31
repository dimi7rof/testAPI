
//using testAPI.Services; // Replace with the actual namespace of your code
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Xml.Linq;
//using Microsoft.Extensions.Logging.Abstractions;
//using Xunit;
//using System.Diagnostics.CodeAnalysis;
//using System.Data.Common;

//public class CarServiceTests
//{
//    private class InMemoryDbConnection : IDbConnection
//    {
//        public string ConnectionString { get; set; }

//        public ConnectionState State => ConnectionState.Open;

//        public int ConnectionTimeout => throw new NotImplementedException();

//        public string Database => throw new NotImplementedException();

//        public IDbTransaction BeginTransaction() => throw new NotImplementedException();

//        public IDbTransaction BeginTransaction(IsolationLevel il) => throw new NotImplementedException();

//        public void ChangeDatabase(string databaseName) => throw new NotImplementedException();

//        public void Close() { }

//        public IDbCommand CreateCommand()
//        {
//            return new SqlCommand();
//        }

//        public void Dispose() { }

//        public void Open() { }
//    }

//    private class FakeDataReader : IDataReader
//    {
//        private readonly List<object[]> _data = new List<object[]>
//        {
//            new object[] { 1, "Toyota", "Camry", 50000 },
//            new object[] { 2, "Honda", "Civic", 60000 }
//        };

//        private int _currentIndex = -1;

//        public object this[string name] => throw new NotImplementedException();

//        public object this[int i] => _data[_currentIndex][i];

//        public int Depth => throw new NotImplementedException();

//        public bool IsClosed => false;

//        public int RecordsAffected => throw new NotImplementedException();

//        public int FieldCount => 4;

//        public void Close() { }

//        public void Dispose() { }

//        public bool GetBoolean(int i) => throw new NotImplementedException();

//        public byte GetByte(int i) => throw new NotImplementedException();

//        public long GetBytes(int i, long fieldOffset, byte[]? buffer, int bufferoffset, int length)
//        {
//            throw new NotImplementedException();
//        }

//        public char GetChar(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public long GetChars(int i, long fieldoffset, char[]? buffer, int bufferoffset, int length)
//        {
//            throw new NotImplementedException();
//        }

//        public IDataReader GetData(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public string GetDataTypeName(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public DateTime GetDateTime(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public decimal GetDecimal(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public double GetDouble(int i)
//        {
//            throw new NotImplementedException();
//        }

//        [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties)]
//        public Type GetFieldType(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public float GetFloat(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public Guid GetGuid(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public short GetInt16(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public int GetInt32(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public long GetInt64(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public string GetName(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public int GetOrdinal(string name)
//        {
//            throw new NotImplementedException();
//        }

//        public DataTable? GetSchemaTable()
//        {
//            throw new NotImplementedException();
//        }

//        public string GetString(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public object GetValue(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public int GetValues(object[] values)
//        {
//            throw new NotImplementedException();
//        }

//        public bool IsDBNull(int i)
//        {
//            throw new NotImplementedException();
//        }

//        public bool NextResult()
//        {
//            throw new NotImplementedException();
//        }

//        // Implement other methods...

//        public bool Read()
//        {
//            _currentIndex++;
//            return _currentIndex < _data.Count;
//        }
//    }

//    [Fact]
//    public void ViewCars_ReturnsCorrectXml()
//    {
//        // Arrange
//        //var connection = new DbConnection();
//        var connection = new InMemoryDbConnection();
//        var command = connection.CreateCommand();
//        var dataReader = new FakeDataReader();
//        var logger = new NullLogger<testService>();

//        var carService = new testService(logger);

//        command.Connection = connection;
//        command.CommandText = "SELECT * FROM Cars";
//        command.ExecuteReader();

//        // Act
//        var result = carService.ViewCars("dummy connection string");

//        // Assert
//        var expectedXml = new XElement("Cars",
//            new XElement("Car",
//                new XAttribute("Id", 1),
//                new XElement("Brand", "Toyota"),
//                new XElement("Model", "Camry"),
//                new XElement("Mileage", 50000)),
//            new XElement("Car",
//                new XAttribute("Id", 2),
//                new XElement("Brand", "Honda"),
//                new XElement("Model", "Civic"),
//                new XElement("Mileage", 60000)));

//        Assert.Equal(expectedXml.ToString(), result.ToString());
//    }
//}

