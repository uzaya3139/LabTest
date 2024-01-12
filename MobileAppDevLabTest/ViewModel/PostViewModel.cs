using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using MobileAppDevLabTest.Models;
using System.Windows.Input;
using Newtonsoft.Json;

namespace MobileAppDevLabTest.ViewModel
{
    public class PostViewModel : BindableObject
    {
        private ObservableCollection<PostRecord> _posts;
        public ObservableCollection<PostRecord> Posts
        {
            get { return _posts; }
            set
            {
                if (_posts != value)
                {
                    _posts = value;
                    OnPropertyChanged(nameof(Posts));
                }
            }
        }

        public Command LoadPostsCommand { get; }
        public Command AddPostCommand { get; }
        public Command UpdatePostCommand { get; }
        public Command DeletePostCommand { get; }


        public PostViewModel()
        {
            Posts = new ObservableCollection<PostRecord>();
            LoadPostsCommand = new Command(async () => await LoadPostsAsync());
            AddPostCommand = new Command(async () => await AddPostAsync());
            UpdatePostCommand = new Command(async () => await UpdatePostAsync());
            DeletePostCommand = new Command(async () => await DeletePostAsync());
        }

        private async Task DeletePostAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var postToDelete = Posts.FirstOrDefault(); // Get the first post as an example

                    if (postToDelete != null)
                    {
                        string apiUrl = $"https://jsonplaceholder.typicode.com/posts/{postToDelete.Id}";

                        // Create the HttpRequestMessage with the DELETE method
                        var request = new HttpRequestMessage(HttpMethod.Delete, apiUrl);

                        // Send the HTTP request
                        var response = await client.SendAsync(request);

                        // Handle the response as needed
                        if (response.IsSuccessStatusCode)
                        {
                            // Successfully deleted the post
                            await LoadPostsAsync(); // Refresh the posts after deleting
                        }
                        else
                        {
                            // Handle error response
                            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log or display an error message
                Console.WriteLine($"Error sending DELETE request: {ex.Message}");
            }
        }

        private async Task AddPostAsync()
        {
            // Implement logic to add a new post
            // For simplicity, let's assume you have an instance of PostRecord for the new post
            var newPost = new PostRecord { Title = "New Post", Body = "Body of the new post" };

            // Perform the POST request to add the new post
            await SendHttpRequestAsync(newPost, HttpMethod.Post);
        }

        private async Task UpdatePostAsync()
        {
            // Implement logic to update an existing post
            // For simplicity, let's assume you have an instance of PostRecord to update
            var postToUpdate = Posts.FirstOrDefault(); // Get the first post as an example

            if (postToUpdate != null)
            {
                // Perform the PUT request to update the post
                await SendHttpRequestAsync(postToUpdate, HttpMethod.Put);
            }
        }

        private async Task SendHttpRequestAsync(PostRecord post, HttpMethod httpMethod)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://jsonplaceholder.typicode.com/posts";

                    // Serialize the post object to JSON
                    var postJson = JsonConvert.SerializeObject(post);
                    var content = new StringContent(postJson, Encoding.UTF8, "application/json");

                    // Create the HttpRequestMessage with the specified method
                    var request = new HttpRequestMessage(httpMethod, apiUrl)
                    {
                        Content = content
                    };

                    // Send the HTTP request
                    var response = await client.SendAsync(request);

                    // Handle the response as needed
                    if (response.IsSuccessStatusCode)
                    {
                        // Successfully added or updated the post
                        await LoadPostsAsync(); // Refresh the posts after adding or updating
                    }
                    else
                    {
                        // Handle error response
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log or display an error message
                Console.WriteLine($"Error sending HTTP request: {ex.Message}");
            }
        }

        private async Task LoadPostsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://jsonplaceholder.typicode.com/posts";
                    var response = await client.GetStringAsync(apiUrl);

                    // Deserialize the response JSON to a collection of PostRecord
                    var posts = JsonConvert.DeserializeObject<ObservableCollection<PostRecord>>(response);

                    // Update the ObservableCollection with the retrieved data
                    Posts.Clear();
                    foreach (var post in posts)
                    {
                        Posts.Add(post);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception, e.g., log or display an error message
                Console.WriteLine($"Error loading posts: {ex.Message}");
            }
        }
    }
}
