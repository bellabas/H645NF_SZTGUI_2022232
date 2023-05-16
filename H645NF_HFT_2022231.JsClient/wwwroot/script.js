let genres = [];
let connection = null;

setUpSignalR();
getGenres();

function setUpSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:31652/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

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
    newElement.innerHTML = `<td colspan="3"><label>Genre value</label>
            <input type="text" id="genreUpdateValue" value="${genres.find(g => g.genreId == id).value}" />
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