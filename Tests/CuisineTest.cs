using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CuisineFinder.Objects
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cuisine_database_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_CuisineIsEmptyAtFirst()
    {
      //Arrange, Act
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Cuisine firstCuisine = new Cuisine(0, "Cuisine of Canada");
      Cuisine secondCuisine = new Cuisine(0, "Cuisine of Canada");

      //Assert
      Assert.Equal(firstCuisine, secondCuisine);
    }
    [Fact]
    public void Test_Save_SavesCuisineToDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine(0, "Cuisine of Canada");
      testCuisine.Save();

      //Act
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
public void Test_Save_AssignsIdToCuisineObject()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine(0, "Cuisine of Canada");
      testCuisine.Save();

      //Act
      Cuisine savedCuisine = Cuisine.GetAll()[0];

      int result = savedCuisine.GetId();
      int testId = testCuisine.GetId();

      //Assert
      Assert.Equal(testId, result);
    }
    [Fact]
public void Test_Find_FindsCuisineInDatabase()
{
  //Arrange
  Cuisine testCuisine = new Cuisine(1, "Cuisine of Canada");
  testCuisine.Save();

  //Act
  Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

  //Assert
  Assert.Equal(testCuisine, foundCuisine);
}

[Fact]
public void Test_GetRestaurants_RetrievesAllRestaurantsWithCuisine()
{
  Cuisine testCuisine = new Cuisine(1, "Cuisine of Canada");
  testCuisine.Save();

  Restaurant firstRestaurant = new Restaurant(testCuisine.GetId(),"Burger Place");
  firstRestaurant.Save();
  Restaurant secondRestaurant = new Restaurant(testCuisine.GetId(),"Pizza Place");
  secondRestaurant.Save();


  List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
  List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();

  Assert.Equal(testRestaurantList, resultRestaurantList);
}
[Fact]
public void Test_Update_UpdatesCuisineInDatabase()
{
  //Arrange
  string name = "Cuisine of Canada";
  Cuisine testCuisine = new Cuisine(0, name);
  testCuisine.Save();
  string newName = "Cuisine of Wyoming";

  //Act
  testCuisine.Update(newName);

  string result = testCuisine.GetName();

  //Assert
  Assert.Equal(newName, result);
}
[Fact]
public void Test_Delete_DeletesCuisineFromDatabase()
{
  //Arrange
  string name1 = "Cuisine of Canada";
  Cuisine testCuisine1 = new Cuisine(1, name1);
  testCuisine1.Save();

  string name2 = "India";
  Cuisine testCuisine2 = new Cuisine(2, name2);
  testCuisine2.Save();

  Restaurant testRestaurant1 = new Restaurant(testCuisine1.GetId(), "Burger Place");
  testRestaurant1.Save();
  Restaurant testRestaurant2 = new Restaurant(testCuisine2.GetId(), "Pizza Place");
  testRestaurant2.Save();

  //Act
  testCuisine1.Delete();
  List<Cuisine> resultCuisines = Cuisine.GetAll();
  List<Cuisine> testCuisineList = new List<Cuisine> {testCuisine2};

  List<Restaurant> resultRestaurants = Restaurant.GetAll();
  List<Restaurant> testRestaurantList = new List<Restaurant> {testRestaurant2};

  //Assert
  Assert.Equal(testCuisineList, resultCuisines);
  Assert.Equal(testRestaurantList, resultRestaurants);
}
    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }

  }
}
