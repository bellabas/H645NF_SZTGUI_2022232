// VARIABLES
let genres = [];
let mvoies = [];
let rents = [];

let connection = null;

// MAIN
setUpSignalR();
getGenres();
getMovies();
getRents();


// SIGNALR
function setUpSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:31652/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    // movie
    connection.on("MovieCreated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getMovies();
    });

    connection.on("MovieDeleted", (user, message) => {
        //console.log(user);
        //console.log(message);
        getMovies();
    });

    connection.on("MovieUpdated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getMovies();
    });

    // genre
    connection.on("GenreCreated", (user, message) =>
    {
        //console.log(user);
        //console.log(message);
        getGenres();
    });

    connection.on("GenreDeleted", (user, message) => {
        //console.log(user);
        //console.log(message);
        getGenres();
    });

    connection.on("GenreUpdated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getGenres();
    });

    // rent
    connection.on("RentCreated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getRents();
    });

    connection.on("RentDeleted", (user, message) => {
        //console.log(user);
        //console.log(message);
        getRents();
    });

    connection.on("RentUpdated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getRents();
    });

    connection.onclose(async () =>
    {
        await start();
    });

    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

// MENU

function showGenre() {
    hideAll();
    document.getElementById('genreDiv').style.display = 'block';
}

function showMovie() {
    hideAll();
    document.getElementById('movieDiv').style.display = 'block';
}

function showRent() {
    hideAll();
    document.getElementById('rentDiv').style.display = 'block';
}

function hideAll() {
    document.getElementById('genreDiv').style.display = 'none';
    document.getElementById('movieDiv').style.display = 'none';
    document.getElementById('rentDiv').style.display = 'none';

}

// MOVIE

async function getMovies() {
    await fetch('http://localhost:31652/movie')
        .then(x => x.json())
        .then(y => {
            movies = y;
            //console.log(movies);
            displayMovies();
        });
}


function displayMovies() {
    document.getElementById('movieResultArea').innerHTML = "";
    movies.forEach(m => {
        document.getElementById('movieResultArea').innerHTML += `<tr id="movieRow${m.movieId}">` +
            '<td>' + m.movieId + '</td>' + '<td>' + m.title + '</td>' + '<td>' + m.runtime + '</td>' + '<td>' + m.year + '</td>' +
            '<td>' + m.country + '</td>' + '<td>' + m.budget + '</td>' + '<td>' + m.genreId + '</td>' +
            `<td><button type="button" onclick="deleteMovie(${m.movieId})">Delete</button>` +
            `<button type="button" onclick="showUpdateMovie(${m.movieId})">Update</button></td></tr>`;
    });
}

function createMovie() {
    let movieCreateTitle = document.getElementById('movieCreateTitle').value;
    let movieCreateRuntime = document.getElementById('movieCreateRuntime').value;
    let movieCreateYear = document.getElementById('movieCreateYear').value;
    let movieCreateCountry = document.getElementById('movieCreateCountry').value;
    let movieCreateBudget = document.getElementById('movieCreateBudget').value;
    let movieCreateGenreId = document.getElementById('movieCreateGenreId').value;
    fetch('http://localhost:31652/movie',
        {
            method: 'POST',
            headers:
            {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    title: movieCreateTitle,
                    runtime: movieCreateRuntime,
                    year: movieCreateYear,
                    country: movieCreateCountry,
                    budget: movieCreateBudget,
                    genreId: movieCreateGenreId
                }),
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getMovies();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function deleteMovie(id) {
    fetch('http://localhost:31652/movie/' + id,
        {
            method: 'DELETE',
            headers:
            {
                'Content-Type': 'application/json',
            },
            body: null
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getMovies();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function showUpdateMovie(id) {
    if (document.getElementById('movieUpdateForm') != null) {
        document.getElementById('movieUpdateForm').remove();
    }

    let movieObject = movies.find(m => m.movieId == id);

    let elementToUpdate = document.getElementById("movieRow" + id);
    let newElement = document.createElement('tr');
    newElement.setAttribute('id', 'movieUpdateForm');
    newElement.innerHTML = `<td colspan="8">
            <h3>Update</h3>
            <label>Title</label>
            <input type="text" id="movieUpdateTitle" value="${movieObject.title}" placeholder="Enter the Title of Movie here" />
            <label>Runtime</label>
            <input type="number" id="movieUpdateRuntime" value="${movieObject.runtime}" placeholder="Enter the Runtime of Movie here" />
            <label>Year</label>
            <input type="number" id="movieUpdateYear" value="${movieObject.year}" placeholder="Enter the release Year of Movie here" />
            <label>Country</label>
            <input type="text" id="movieUpdateCountry" value="${movieObject.country}" placeholder="Enter the Country where the Movie was filmed here" />
            <label>Budget</label>
            <input type="number" id="movieUpdateBudget" value="${movieObject.budget}" placeholder="Enter the Budget of Movie here" />
            <label>GenreId</label>
            <input type="number" id="movieUpdateGenreId" value="${movieObject.genreId}" placeholder="Enter the GenreId of Movie here" />
            <button type="button" onclick="updateMovie(${id})">Update Movie</button></td>`;

    elementToUpdate.parentElement.insertBefore(newElement, elementToUpdate.nextElementSibling);
}

function updateMovie(id) {
    let movieUpdateTitle = document.getElementById('movieUpdateTitle').value;
    let movieUpdateRuntime = document.getElementById('movieUpdateRuntime').value;
    let movieUpdateYear = document.getElementById('movieUpdateYear').value;
    let movieUpdateCountry = document.getElementById('movieUpdateCountry').value;
    let movieUpdateBudget = document.getElementById('movieUpdateBudget').value;
    let movieUpdateGenreId = document.getElementById('movieUpdateGenreId').value;

    fetch('http://localhost:31652/movie',
        {
            method: 'PUT',
            headers:
            {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    movieId: id,
                    title: movieUpdateTitle,
                    runtime: movieUpdateRuntime,
                    year: movieUpdateYear,
                    country: movieUpdateCountry,
                    budget: movieUpdateBudget,
                    genreId: movieUpdateGenreId
                }),
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getMovies();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}


// GENRE

async function getGenres() {
    await fetch('http://localhost:31652/genre')
        .then(x => x.json())
        .then(y => {
            genres = y;
            //console.log(genres);
            displayGenres();
        });
}


function displayGenres() {
    document.getElementById('genreResultArea').innerHTML = "";
    genres.forEach(g => {
        document.getElementById('genreResultArea').innerHTML += `<tr id="genreRow${g.genreId}"><td>` + g.genreId + '</td><td>' + g.value + `</td><td><button type="button" onclick="deleteGenre(${g.genreId})">Delete</button><button type="button" onclick="showUpdateGenre(${g.genreId})">Update</button></td></tr>`;
    });
}

function createGenre() {
    let genreCreateValue = document.getElementById('genreCreateValue').value;
    fetch('http://localhost:31652/genre',
        {
            method: 'POST',
            headers:
            {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
            {
                value: genreCreateValue
            }),
        })
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getGenres();
        })
        .catch((error) =>
        {
            console.error('Error:', error);
        });
}

function deleteGenre(id) {
    fetch('http://localhost:31652/genre/' + id,
        {
            method: 'DELETE',
            headers:
            {
                'Content-Type': 'application/json',
            },
            body: null
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getGenres();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function showUpdateGenre(id) {
    if (document.getElementById('genreUpdateForm') != null) {
        document.getElementById('genreUpdateForm').remove();
    }
    let elementToUpdate = document.getElementById("genreRow" + id);

    let newElement = document.createElement('tr');
    newElement.setAttribute('id', 'genreUpdateForm');
    newElement.innerHTML = `<td colspan="3"><h3>Update</h3><label>Genre value</label>
            <input type="text" id="genreUpdateValue" placeholder="Enter the value of Genre here" value="${genres.find(g => g.genreId == id).value}" />
            <button type="button" onclick="updateGenre(${id})">Update Genre</button></td>`;

    elementToUpdate.parentElement.insertBefore(newElement, elementToUpdate.nextElementSibling);
}

function updateGenre(id) {
    let genreUpdateValue = document.getElementById('genreUpdateValue').value;
    fetch('http://localhost:31652/genre',
        {
            method: 'PUT',
            headers:
            {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    genreId: id,
                    value: genreUpdateValue
                }),
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getGenres();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}


// RENT

async function getRents() {
    await fetch('http://localhost:31652/rent')
        .then(x => x.json())
        .then(y => {
            rents = y;
            //console.log(rents);
            displayRents();
        });
}


function displayRents() {
    document.getElementById('rentResultArea').innerHTML = "";
    rents.forEach(r => {
        document.getElementById('rentResultArea').innerHTML += `<tr id="rentRow${r.rentId}">` + '<td>' + r.rentId + '</td>' +
            '<td>' + r.name + '</td>' + '<td>' + r.age + '</td>' + '<td>' + r.gender + '</td>' + '<td>' + r.country + '</td>' +
            '<td>' + r.rating + '</td>' + '<td>' + r.start + '</td>' + '<td>' + r.end + '</td>' + '<td>' + r.interval + '</td>' +
            '<td>' + r.movieId + '</td>' +
            `<td><button type="button" onclick="deleteRent(${r.rentId})">Delete</button>` +
            `<button type="button" onclick="showUpdateRent(${r.rentId})">Update</button></td></tr>`;
    });
}

function createRent() {
    let rentCreateName = document.getElementById('rentCreateName').value;
    let rentCreateAge = document.getElementById('rentCreateAge').value;
    let rentCreateGender = document.getElementById('rentCreateGender').value;
    let rentCreateCountry = document.getElementById('rentCreateCountry').value;
    let rentCreateRating = document.getElementById('rentCreateRating').value;
    let rentCreateStart = document.getElementById('rentCreateStart').value;
    let rentCreateEnd = document.getElementById('rentCreateEnd').value;
    let rentCreateMovieId = document.getElementById('rentCreateMovieId').value;

    fetch('http://localhost:31652/rent',
        {
            method: 'POST',
            headers:
            {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    name: rentCreateName,
                    age: rentCreateAge,
                    gender: rentCreateGender,
                    country: rentCreateCountry,
                    rating: rentCreateRating,
                    start: rentCreateStart,
                    end: rentCreateEnd,
                    movieId: rentCreateMovieId
                }),
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getRents();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function deleteRent(id) {
    fetch('http://localhost:31652/rent/' + id,
        {
            method: 'DELETE',
            headers:
            {
                'Content-Type': 'application/json',
            },
            body: null
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getRents();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function showUpdateRent(id) {
    if (document.getElementById('rentUpdateForm') != null) {
        document.getElementById('rentUpdateForm').remove();
    }

    let rentObject = rents.find(r => r.rentId == id);
    console.log(rentObject.start);
    console.log(rentObject.end);

    let elementToUpdate = document.getElementById("rentRow" + id);
    let newElement = document.createElement('tr');
    newElement.setAttribute('id', 'rentUpdateForm');
    newElement.innerHTML = `<td colspan="11">
            <h3>Update</h3>
            <label>Name</label>
            <input type="text" id="rentUpdateName" value="${rentObject.name}" placeholder="Enter the Renter's Name here" />
            <label>Age</label>
            <input type="number" id="rentUpdateAge" value="${rentObject.age}" placeholder="Enter the Renter's Age here" />
            <label>Gender</label>
            <input type="text" id="rentUpdateGender" value="${rentObject.gender}" placeholder="Enter the Renter's Gender here" />
            <label>Country</label>
            <input type="text" id="rentUpdateCountry" value="${rentObject.country}" placeholder="Enter the Renter's Country here" />
            <label>Rating</label>
            <input type="number" id="rentUpdateRating" value="${rentObject.rating}" placeholder="Enter the Renter's movie Rating here" />
            <label>Start</label>
            <input type="date" id="rentUpdateStart" value="${rentObject.start.substring(0,10)}" placeholder="Enter the Rental's Start here" />
            <label>End</label>
            <input type="date" id="rentUpdateEnd" value="${rentObject.end.substring(0,10)}" placeholder="Enter the Rental's End here" />
            <label>MovieId</label>
            <input type="number" id="rentUpdateMovieId" value="${rentObject.movieId}" placeholder="Enter the ID of Rented Movie here" />
            <button type="button" onclick="updateRent(${id})">Update Rent</button></td>`;

    elementToUpdate.parentElement.insertBefore(newElement, elementToUpdate.nextElementSibling);
}

function updateRent(id) {
    let rentUpdateName = document.getElementById('rentUpdateName').value;
    let rentUpdateAge = document.getElementById('rentUpdateAge').value;
    let rentUpdateGender = document.getElementById('rentUpdateGender').value;
    let rentUpdateCountry = document.getElementById('rentUpdateCountry').value;
    let rentUpdateRating = document.getElementById('rentUpdateRating').value;
    let rentUpdateStart = document.getElementById('rentUpdateStart').value;
    let rentUpdateEnd = document.getElementById('rentUpdateEnd').value;
    let rentUpdateMovieId = document.getElementById('rentUpdateMovieId').value;

    fetch('http://localhost:31652/rent',
        {
            method: 'PUT',
            headers:
            {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                {
                    rentId: id,
                    name: rentUpdateName,
                    age: rentUpdateAge,
                    gender: rentUpdateGender,
                    country: rentUpdateCountry,
                    rating: rentUpdateRating,
                    start: rentUpdateStart,
                    end: rentUpdateEnd,
                    movieId: rentUpdateMovieId
                }),
        })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getRents();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

