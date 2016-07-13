using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace CuisineFinder.Objects
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private int _cuisineId;

    public Restaurant(int CuisineId, string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _cuisineId = CuisineId;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
        if (!(otherRestaurant is Restaurant))
        {
          return false;
        }
        else
        {
          Restaurant newRestaurant = (Restaurant) otherRestaurant;
          bool idEquality = this.GetId() == newRestaurant.GetId();
          bool nameEquality = this.GetName() == newRestaurant.GetName();
          bool cuisineEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
          return (idEquality && nameEquality && cuisineEquality);
        }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public void SetCuisineId(int newCuisineId)
    {
      _cuisineId = newCuisineId;
    }
  public static List<Restaurant> GetAll()
  {
    List<Restaurant> AllRestaurants = new List<Restaurant>{};

    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int restaurantId = rdr.GetInt32(0);
      int restaurantCuisineId = rdr.GetInt32(1);
      string restaurantName = rdr.GetString(2);

      Restaurant newRestaurant = new Restaurant(restaurantCuisineId, restaurantName);
      AllRestaurants.Add(newRestaurant);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return AllRestaurants;
  }
  public void Save()
  {
    SqlConnection conn = DB.Connection();
    SqlDataReader rdr;
    conn.Open();

    SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (cuisine_id, name) OUTPUT INSERTED.id VALUES (@RestaurantCuisineId, @RestaurantName);", conn);

    SqlParameter nameParameter = new SqlParameter();
    nameParameter.ParameterName = "@RestaurantName";
    nameParameter.Value = this.GetName();

    SqlParameter cuisineIdParameter = new SqlParameter();
    cuisineIdParameter.ParameterName = "@RestaurantCuisineId";
    cuisineIdParameter.Value = this.GetCuisineId();

    cmd.Parameters.Add(nameParameter);
    cmd.Parameters.Add(cuisineIdParameter);


    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      this._id = rdr.GetInt32(0);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
  }

  public static Restaurant Find(int id)
  {
    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);
    SqlParameter restaurantIdParameter = new SqlParameter();
    restaurantIdParameter.ParameterName = "@RestaurantId";
    restaurantIdParameter.Value = id.ToString();
    cmd.Parameters.Add(restaurantIdParameter);
    rdr = cmd.ExecuteReader();

    int foundRestaurantId = 0;
    string foundRestaurantName = null;
    int foundRestaurantCuisineId = 0;

    while(rdr.Read())
    {
      foundRestaurantId = rdr.GetInt32(0);
      foundRestaurantName = rdr.GetString(2);
      foundRestaurantCuisineId = rdr.GetInt32(1);
    }
    Restaurant foundRestaurant = new Restaurant(foundRestaurantCuisineId, foundRestaurantName);

    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return foundRestaurant;
  }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }


  }
}
