function openManageShowtimesModal(cinemaId, cinemaName, cinemaLocation) {
  fetch(`/Manager/ShowtimeSetup/GetMoviesWithShowtimes?cinemaId=${cinemaId}`)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Failed to load showtimes.");
      }
      return response.json();
    })
    .then((movies) => {
      renderShowtimesModal(movies, cinemaName, cinemaLocation, cinemaId);
      $("#manageShowtimesModal").modal("show");
    })
    .catch((error) => {
      console.error("Error loading showtimes:", error);
      Swal.fire({
        title: "Error",
        text: "An error occurred while loading showtimes.",
        icon: "error",
        confirmButtonColor: "#dc3545",
      });
    });
}

function renderShowtimesModal(movies, cinemaName, cinemaLocation, cinemaId) {
  let modalHtml = `
        <div id="manageShowtimesModal" class="modal fade" tabindex="-1" aria-labelledby="manageShowtimesModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content bg-dark text-white">
                    <div class="modal-header border-0">
                        <h5 class="modal-title fw-bold">
                            <i class="bi bi-clock-history"></i> ${cinemaName} - <span class="text-muted">${cinemaLocation}</span>
                        </h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="table-responsive">
                            <table class="table table-dark table-striped text-center">
                                <thead class="table-warning text-dark">
                                    <tr>
                                        <th>Movie Title</th>
                                        <th>Showtimes</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>`;

  movies.forEach((movie) => {
    modalHtml += `
            <tr>
                <td class="fw-bold">${movie.title}</td>
                <td>
                    <div class="btn-group-toggle" data-toggle="buttons">
                        ${[12, 15, 18, 20, 22]
                          .map((hour) => {
                            const isSelected = movie.showtimes.includes(hour)
                              ? "active"
                              : "";
                            return `
                                <label class="btn btn-outline-light btn-sm ${isSelected}">
                                    <input type="checkbox" class="showtime-checkbox" data-movie-id="${
                                      movie.id
                                    }" data-hour="${hour}" ${
                              isSelected ? "checked" : ""
                            }> ${hour}:00
                                </label>`;
                          })
                          .join("")}
                    </div>
                </td>
                <td>
                    <button class="btn btn-primary btn-sm" onclick="updateShowtimes('${
                      movie.id
                    }', '${cinemaId}')">
                        <i class="bi bi-save"></i> Save
                    </button>
                </td>
            </tr>`;
  });

  modalHtml += `</tbody></table></div>
                    </div>
                    <div class="modal-footer border-0">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>`;

  document.getElementById("manageShowtimesModalContainer").innerHTML =
    modalHtml;
  $("#manageShowtimesModal").modal("show");
}

function updateShowtimes(movieId, cinemaId) {
  let selectedShowtimes = [];
  document
    .querySelectorAll(`.showtime-checkbox[data-movie-id="${movieId}"]:checked`)
    .forEach((checkbox) => {
      selectedShowtimes.push(parseInt(checkbox.dataset.hour, 10));
    });

  fetch("/Manager/api/ShowtimeApi/UpdateShowtimes", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      CinemaId: cinemaId,
      MovieId: movieId,
      Showtimes: selectedShowtimes,
    }),
  })
    .then((response) => {
      if (!response.ok) throw new Error("Failed to update showtimes.");
      return;
    })
    .then((_) => {
      Swal.fire({
        title: "Success",
        text: "Showtimes updated successfully!",
        icon: "success",
        confirmButtonColor: "#28a745",
        timer: 2000,
        showConfirmButton: false,
        toast: true,
        position: "top-end",
      });
    })
    .catch((error) => {
      console.error("Error:", error);
      Swal.fire({
        title: "Error",
        text: "An error occurred while updating showtimes.",
        icon: "error",
        confirmButtonColor: "#dc3545",
      });
    });
}
