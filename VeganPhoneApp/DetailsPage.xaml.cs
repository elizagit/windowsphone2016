﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using VeganPhoneApp.Resources;
using Newtonsoft.Json;
using VeganPhoneApp.Models;
using VeganPhoneApp.ViewModels;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace VeganPhoneApp
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();

            //DataContext = App.ViewModel;
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    DataContext = App.ViewModel.Items[index];
                   
                }
                           }
        }
             private void txtinput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

         private async void AddRating_Click(object sender, RoutedEventArgs e)
        {
            //string newRating = txtinput.Text;

           string name = Name.Text;


            HttpClient client = new HttpClient();


            client.BaseAddress = new Uri("http://169.254.21.12");

            var url = "api/Restaurant?name=" + name;

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);


            var data = response.Content.ReadAsStringAsync();
            var yourResult = JsonConvert.DeserializeObject<Restaurant>(data.Result.ToString());

            int NewRating = Convert.ToInt32(txtinput.Text);
            int NewSumOfRatings = yourResult.SumOfRatings + NewRating;
            yourResult.NumberOfRatings++;


            double UpdatedRatingBefore = NewSumOfRatings / yourResult.NumberOfRatings;
            double UpdatedRatingAfter = Math.Round(UpdatedRatingBefore, 0);
            int UpdateDatabase = Convert.ToInt32(UpdatedRatingAfter);

            var updatedRestaurant = new Restaurant() { RestaurantID = yourResult.RestaurantID, RestaurantName = yourResult.RestaurantName, Rating = UpdateDatabase, SumOfRatings = NewSumOfRatings, NumberOfRatings = yourResult.NumberOfRatings};

            response = await client.PutAsJsonAsync("api/Restaurant?name=" + name, updatedRestaurant);
         // if (response.IsSuccessStatusCode)
          //{
             // Uri HealthyHabitsUrl = response.Headers.Location;

              // HTTP PUT
              //HealthyHabits.Rating = 4;   // Update price
              //response = await client.PutAsJsonAsync(HealthyHabitsUrl, HealthyHabits);

              // HTTP DELETEa
              //response = await client.DeleteAsync(HealthyHabitsUrl);
          
            txtop.Text = UpdateDatabase.ToString();




           
             
          
         }

        }








       



            
            
               
               /* if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
                 */
            }  
    
        

    

      
         /* try
          {
              Restaurant restaurant = new Restaurant();


             
              restaurant.Rating = Convert.ToInt32(txtinput.Text);
              

              string jsonData = JsonConvert.SerializeObject(restaurant);

              WebClient webClient = new WebClient();
              webClient.Headers["Content-type"] = "application/json";
              webClient.Encoding = System.Text.Encoding.UTF8;

              Uri uri = new Uri("http://localhost:27448/api/restaurant/",  UriKind.Absolute);
              webClient.UploadStringCompleted += new UploadStringCompletedEventHandler(webClient_UploadStringCompleted);  
              webClient.UploadStringAsync(uri, "POST", jsonData);
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message);
          }
      }
        void webClient_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
{
   try
   {
       Restaurant restaurant = JsonConvert.DeserializeObject<Restaurant>(e.Result);
   }
   catch (Exception ex)
   {
      MessageBox.Show(ex.Message);
   }
}
private int Int32(string p)
{
 	throw new NotImplementedException();
}
      
  }


 }
  
   
     */
        

      
