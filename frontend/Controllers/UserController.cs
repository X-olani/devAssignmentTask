using frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            // 
            List<UserViewModel> users = new List<UserViewModel>();

            //try catch statement to pull from api 
            try
            {



                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7018/api/");

                //authorization is used for the http client call 
                APISecurity.InitHead(client);


                var response = client.GetAsync("users");
                response.Wait();

                //get the data from the api request 
                var result = response.Result;

                //Array object of  User  to store api data request



                //if call was successfull
                if (result.IsSuccessStatusCode)
                {
                    //get request in JSON suing a async
                    var getData = result.Content.ReadFromJsonAsync<List<UserViewModel>>();
                    getData.Wait();

                    //getData is in array object from  jsonasync task 
                    users = getData.Result;
                    ;
                }
                //pass array list to view to be displayed 
                return View(users);

            }
            //if api connection not made send error to view
            catch (Exception e)
            {
                ViewBag.Error = "Error";
                return View(users);
            }

        }


        //View for create user 
        public ViewResult Create()
        {

            return View();
        }

        [HttpPost]
        //view is paassed an user object from the form
        public async Task< IActionResult> Create(UserViewModel obj)
        {

            // is the form item vaild
            if (ModelState.IsValid)
            {


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7018/api/");
                APISecurity.InitHead(client);


                var response = await client.PostAsJsonAsync("users/create", obj);
                var result = response;
                List<UserViewModel> users = new List<UserViewModel>();

                //checks if request was succuessful if so redirect to index page to show list
                if (result.IsSuccessStatusCode)
                {


                    return RedirectToAction("Index");


                }
            }
            return View();

        }



        public async Task<ViewResult> Edit(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7018/api/");
            APISecurity.InitHead(client);


            var response = client.GetAsync("users/getuser/"+id);
            var result = response.Result;

            UserViewModel users = new UserViewModel();
            if (result.IsSuccessStatusCode)
            {
                 users = await result.Content.ReadFromJsonAsync<UserViewModel>();
               
            }

            return View(users);


        }

        [HttpPost]
        //pass the User object from form
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7018/api/"); 
                APISecurity.InitHead(client);


                var response = await client.PutAsJsonAsync("users/updateuser/"+user.Id, user);
                var result = response;

               //if request was successful redirect to index
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(user);


        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7018/api/");
            APISecurity.InitHead(client);


            var response = await client.DeleteAsync("users/delete/" +id);
            var result = response;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
           

    }
    
}
