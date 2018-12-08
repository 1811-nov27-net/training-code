using System.Collections.Generic;

namespace RestaurantReviews.Library
{
    /// <summary>
    /// A repository managing data access for restaurant objects and their review members.
    /// </summary>
    public interface IRestaurantRepository
    {
        /// <summary>
        /// Get all restaurants with deferred execution.
        /// </summary>
        /// <returns>The collection of restaurants</returns>
        IEnumerable<Restaurant> GetRestaurants();

        /// <summary>
        /// Add a restaurant, including any associated reviews.
        /// </summary>
        /// <param name="restaurant">The restaurant</param>
        void AddRestaurant(Restaurant restaurant);

        /// <summary>
        /// Delete a restaurant by ID. Any reviews associated to it will not be deleted.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant</param>
        void DeleteRestaurant(int restaurantId);

        /// <summary>
        /// Update a restaurant. Will not process any changes to its reviews.
        /// </summary>
        /// <param name="restaurant">The restaurant with changed values</param>
        void UpdateRestaurant(Restaurant restaurant);

        /// <summary>
        /// Add a review, and optionally associate it with a restaurant.
        /// </summary>
        /// <param name="review">The review</param>
        /// <param name="restaurant">The restaurant (or null if none)</param>
        void AddReview(Review review, Restaurant restaurant);

        /// <summary>
        /// Delete a review by ID.
        /// </summary>
        /// <param name="reviewId">The ID of the review</param>
        void DeleteReview(int reviewId);

        /// <summary>
        /// Update a review.
        /// </summary>
        /// <param name="review">The review with changed values</param>
        void UpdateReview(Review review);

        /// <summary>
        /// Persist changes to the data source.
        /// </summary>
        void Save();
    }
}
