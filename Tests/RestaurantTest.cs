using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CuisineFinder.Objects
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cuisine_database_test;Integrated Security=SSPI;";
    }



    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfNamesAreTheSame()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant(1, "Canadian Burgers");
      Restaurant secondRestaurant = new Restaurant(1, "Canadian Burgers");

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant(1, "Canadian Burgers");

      //Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant(1, "Canadian Burgers");

      //Act
      testRestaurant.Save();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant(1, "Canadian Burgers");
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());
      Console.WriteLine("testRestaurant: " + testRestaurant.GetId());
      Console.WriteLine("foundRestaurant: " + foundRestaurant.GetId());


      //Assert
      Assert.Equal(testRestaurant, foundRestaurant);
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameDescription()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant(1, "Canadian Burgers");
      Restaurant secondRestaurant = new Restaurant(1, "Canadian Burgers");

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant(1, "Canadian Burgers");
      testRestaurant.Save();

      //Act
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }


    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
