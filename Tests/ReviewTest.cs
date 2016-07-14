// using Xunit;
// using System.Collections.Generic;
// using System;
// using System.Data;
// using System.Data.SqlClient;
//
// namespace CuisineFinder.Objects
// {
//   public class ReviewTest : IDisposable
//   {
//     public ReviewTest()
//     {
//       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cuisine_database_test;Integrated Security=SSPI;";
//     }
//     [Fact]
//     public void Test_Find_FindsRestaurantInDatabase()
//     {
//       //Arrange
//       Review testReview = new Review(3, "It was good.", 2);
//       testReview.Save();
//
//       //Act
//       Review foundReview = Review.Find(testReview.GetId());
//
//       //Assert
//       Assert.Equal(testReview, foundReview);
//     }
//     public void Dispose()
//     {
//       Review.DeleteAll();
//     }
//   }
// }
