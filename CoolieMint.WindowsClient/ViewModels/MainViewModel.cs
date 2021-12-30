using CoolieMint.WindowsClient.Models;
using CoolieMint.WindowsClient.Services.CoolieMint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoolieMint.WindowsClient.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IPackageService _packageService;

        public MainViewModel(IPackageService packageService)
        {
            SelectedPackageName = "nothing selected";
            _packageService = packageService ?? throw new ArgumentNullException(nameof(packageService));
        }

        public string PathToNewPackage
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string SelectedPackageName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public async Task VerifyPackage()
        {
            var version = _packageService.InspectPackage(PathToNewPackage);
            if(version == null)
            {
                PathToNewPackage = null;
                SelectedPackageName = "no valid package";
                return;
            }

            SelectedPackageName = $"CoolieMint {version}";
            await Task.CompletedTask;
            // Ask if version is suitable.
            // Return if there are migration issues.
        }

        public void UploadPackage()
        {
            Task.Run(async () =>
            {
                HttpClient client = new HttpClient();
                // Update port # in the following line.
                client.BaseAddress = new Uri("http://localhost:59719/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    
                    var upgradeModel = new UpgradeModel()
                    {
                        Version = _packageService.InspectPackage(PathToNewPackage).ToString(),
                        Content = File.ReadAllBytes(PathToNewPackage)
                    };

                    HttpResponseMessage response = await client.PostAsJsonAsync($"api/v2/Upgrade", upgradeModel);
                    response.EnsureSuccessStatusCode();

                    // Deserialize the updated product from the response body.
                    //product = await response.Content.ReadAsAsync<Product>();

                    //var url = await CreateProductAsync(product);
                    //Console.WriteLine($"Created at {url}");

                    //// Get the product
                    //product = await GetProductAsync(url.PathAndQuery);
                    //ShowProduct(product);

                    //// Update the product
                    //Console.WriteLine("Updating price...");
                    //product.Price = 80;
                    //await UpdateProductAsync(product);

                    //// Get the updated product
                    //product = await GetProductAsync(url.PathAndQuery);
                    //ShowProduct(product);

                    //// Delete the product
                    //var statusCode = await DeleteProductAsync(product.Id);
                    //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            });
        }
    }
}
