function getEventData() {
  var eventData =  [
    {
      "id": 1,
      "creator_id": 1,
      "name": "Random Eventвавввввввввввввввввв",
      "description": "Random Description",
      "created_at": "2023-10-21T11:33:21.969231Z",
      "start_at": "2022-01-01T12:00:00Z",
      "address": "Random Address",
      "price": 10.99,
      "image": "img/photo_2023-02-04_14-32-18.jpg",
      "category": {
        "id": 1,
        "name": "Лекция"
      }
    },
    {
      "id": 2,
      "creator_id": 1,
      "name": "Random Event 2",
      "description": "Random Description",
      "created_at": "2023-10-21T11:33:29.074884Z",
      "start_at": "2022-01-01T12:00:00Z",
      "address": "Random Address",
      "price": 10.99,
      "image": "img/photo_2023-02-16_09-27-20.jpg",
      "category": {
        "id": 1,
        "name": "Лекция"
      }
    },
    {
      "id": 3,
      "creator_id": 1,
      "name": "Random Event 3",
      "description": "Random Description",
      "created_at": "2023-10-21T11:33:31.045123Z",
      "start_at": "2022-01-01T12:00:00Z",
      "address": "Random Address",
      "price": 10.99,
      "image": "img/photo_2023-08-12_14-16-57.jpg",
      "category": {
        "id": 1,
        "name": "Лекция"
      }
    },
    {
      "id": 4,
      "creator_id": 1,
      "name": "Random Event 4",
      "description": "Random Description",
      "created_at": "2023-10-21T11:33:33.430823Z",
      "start_at": "2022-01-01T12:00:00Z",
      "address": "Random Address",
      "price": 10.99,
      "image": "img/photo_2023-10-22_12-19-46.jpg",
      "category": {
        "id": 1,
        "name": "Лекция"
      }
    },
    {
      "id": 5,
      "creator_id": 1,
      "name": "Random Event 5",
      "description": "Random Description1фффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффф",
      "created_at": "2023-10-21T11:33:34.930724Z",
      "start_at": "2022-01-01T12:00:00Z",
      "address": "Random Address",
      "price": 10.99,
      "image": "img/photo_2023-10-22_12-19-46.jpg",
      "category": {
        "id": 1,
        "name": "Лекция"
      }
    },
    {
      "id": 6,
      "creator_id": 1,
      "name": "Random Event 5",
      "description": "Random Description1фффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффф",
      "created_at": "2023-10-21T11:33:34.930724Z",
      "start_at": "2022-01-01T12:00:00Z",
      "address": "Random Address",
      "price": 10.99,
      "image": "img/photo_2023-10-22_12-19-46.jpg",
      "category": {
        "id": 1,
        "name": "Лекция"
      }
    },
    {
      "id": 7,
      "creator_id": 1,
      "name": "Random Event 5",
      "description": "Random Description1фффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффф",
      "created_at": "2023-10-21T11:33:34.930724Z",
      "start_at": "2022-01-01T12:00:00Z",
      "address": "Random Address",
      "price": 10.99,
      "image": "img/photo_2023-10-22_12-19-46.jpg",
      "category": {
        "id": 1,
        "name": "Лекция"
      }
    },
    {
      "id": 8,
      "creator_id": 1,
      "name": "Random Event 5",
      "description": "Random Description1фффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффффф",
      "created_at": "2023-10-21T11:33:34.930724Z",
      "start_at": "2022-01-01T12:00:00Z",
      "address": "Random Address",
      "price": 10.99,
      "image": "img/photo_2023-10-22_12-19-46.jpg",
      "category": {
        "id": 1,
        "name": "Лекция"
      }
    }
  ]

  for (var i = 0; i < eventData.length; i++) {
    var eventName = eventData[i].name.length > 20 ? eventData[i].name.substring(0, 20) + "..." : eventData[i].name;
    var eventDescription = eventData[i].description.length > 35 ? eventData[i].description.substring(0, 20) + "..." : eventData[i].description;
    var eventDate = eventData[i].date;
    var eventImage = eventData[i].image;
    var eventPrice = eventData[i].price;

    if (eventImage) {
      var eventDate = eventData[i].start_at;
      var imageTag = "<img src='" + eventImage + "' alt='Изображение мероприятия' class='card-img-top' style='max-width: 615px; max-height: 200px;'>";
      var button = "<div class='col'>" +
     "<div class='card'>" +
     imageTag +
     "<div class='card-body'>" +
     "<a class='btn btn-primary' href='1.html' role='button'>" + eventName + "</a>" +
     "<p class='card-text'>" + eventDescription + "</p>" +
     "<p class='card-date'>" + eventDate + "</p>" +
     "<p class='card-price'>Price: " + eventPrice + "</p>" +
     "</div>" +
     "</div>" +
     "</div>";
      $("#event-buttons").append(button);
    }
  }
}

$(document).ready(function () {
  getEventData();
});