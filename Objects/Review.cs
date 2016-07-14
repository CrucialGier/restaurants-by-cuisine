using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace CuisineFinder.Objects
{
  public class Review
  {
    private int _id;
    private int _star;
    private string _comment;
    private int _restaurantId;

    public Review(int Star, string Comment, int RestaurantId, int Id = 0)
    {
      _id = Id;
      _star = Star;
      _comment = Comment;
      _restaurantId = RestaurantId;
    }
    public override bool Equals(System.Object otherReview)
    {
      if (!(otherReview is Review))
      {
        return false;
      }
      else
      {
        Review newReview = (Review) otherReview;
        bool idEquality = this.GetId() == newReview.GetId();
        bool starEquality = this.GetStar() == newReview.GetStar();
        bool commentEquality = this.GetComment() == newReview.GetComment();
        bool restaurantIdEquality = this.GetRestaurantId() == newReview.GetRestaurantId();
        return (idEquality && starEquality && commentEquality && restaurantIdEquality);
      }
    }
    public int GetId()
    {
      return _id;
    }
    public int GetStar()
    {
      return _star;
    }
    public void SetStar(int newStar)
    {
      _star = newStar;
    }
    public string GetComment()
    {
      return _comment;
    }
    public void SetComment(string newComment)
    {
      _comment = newComment;
    }
    public int GetRestaurantId()
    {
      return _restaurantId;
    }
    public void SetRestaurantId(int newRestaurantId)
    {
      _restaurantId = newRestaurantId;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO reviews (stars, comment, restaurant_id) OUTPUT INSERTED.id VALUES (@ReviewStars, @ReviewComment, @ReviewRestaurantId);", conn);

      SqlParameter starsParameter = new SqlParameter();
      starsParameter.ParameterName = "@ReviewStars";
      starsParameter.Value = this.GetStar();

      SqlParameter commentParameter = new SqlParameter();
      commentParameter.ParameterName = "@ReviewComment";
      commentParameter.Value = this.GetComment();

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@ReviewRestaurantId";
      restaurantIdParameter.Value = this.GetRestaurantId();


      cmd.Parameters.Add(starsParameter);
      cmd.Parameters.Add(commentParameter);
      cmd.Parameters.Add(restaurantIdParameter);

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
    public static Review Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE id = @ReviewId;", conn);
      SqlParameter reviewIdParameter = new SqlParameter();
      reviewIdParameter.ParameterName = "@ReviewId";
      reviewIdParameter.Value = id.ToString();
      cmd.Parameters.Add(reviewIdParameter);
      rdr = cmd.ExecuteReader();

      int foundReviewId = 0;
      int foundReviewStar = 0;
      string foundReviewComment = null;
      int foundReviewRestaurantId = 0;

      while(rdr.Read())
      {
        foundReviewId = rdr.GetInt32(0);
        foundReviewStar = rdr.GetInt32(1);
        foundReviewComment = rdr.GetString(2);
        foundReviewRestaurantId = rdr.GetInt32(3);

      }
      Review foundReview = new Review(foundReviewStar, foundReviewComment, foundReviewRestaurantId, foundReviewId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundReview;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM reviews;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
