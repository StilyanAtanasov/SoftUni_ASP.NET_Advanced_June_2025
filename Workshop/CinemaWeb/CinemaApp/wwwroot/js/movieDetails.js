document.addEventListener(`DOMContentLoaded`, () => {
  const addToWatchlistBtn = document.getElementById(`add-to-watchlist-btn`);

  document.querySelectorAll(`.view-details-btn`).forEach(button => {
    button.addEventListener(`click`, async e => {
      const movieId = e.currentTarget.dataset.movieId;
      const isInWatchlist = button.dataset.isInWatchlist;
      const url = `/Movie/DetailsPartial/${movieId}`;

      try {
        const response = await fetch(url, { method: `GET`, cache: `no-store` });
        if (!response.ok) {
          throw new Error(`Failed to load details: ${response.status}`);
        }

        const partialHtml = await response.text();

        const modalElement = document.getElementById(`movieDetailsModal`);

        const movieDetailsContent = document.getElementById(`movieDetailsContent`);
        movieDetailsContent.innerHTML = partialHtml;
        movieDetailsContent.dataset.movieId = movieId;

        const tempDiv = document.createElement("div");
        tempDiv.innerHTML = partialHtml.trim();

        document.getElementById(`movieDetailsLabel`).textContent = tempDiv.querySelector(`.movie-name`).textContent;

        addToWatchlistBtn.style.display = isInWatchlist === `True` ? `none` : `block`;

        const modal = bootstrap.Modal.getOrCreateInstance(modalElement);
        modal.show();
      } catch (error) {
        console.error(error);
        Swal.fire({
          title: `Error`,
          text: `Error occured!`,
          icon: `error`,
          confirmButtonColor: `#dc3545`,
        });
      }
    });
  });

  addToWatchlistBtn.addEventListener(`click`, async function () {
    const movieId = addToWatchlistBtn.closest(`.modal-content`).querySelector(`#movieDetailsContent`).dataset.movieId;

    try {
      const response = await fetch(`/api/WatchlistApi/Add?movieId=${movieId}`, {
        method: `POST`,
        headers: {
          "Content-Type": `application/json`,
        },
      });

      if (response.ok) {
        // SweetAlert success
        Swal.fire({
          title: `Success!`,
          text: `Movie was added to your watchlist!`,
          icon: `success`,
          confirmButtonColor: `#28a745`,
        });

        removeAddToWatchlistButton(addToWatchlistBtn, movieId);
      } else if (response.status === 400) {
        Swal.fire({
          title: `Oops`,
          text: `Movie is already in your watchlist!`,
          icon: `info`,
          confirmButtonColor: `#aa3`,
        });

        removeAddToWatchlistButton(addToWatchlistBtn, movieId);
      } else {
        throw new Error(`Error adding movie to your watchlist!`);
      }
    } catch (err) {
      Swal.fire({
        title: `Error`,
        text: err.message,
        icon: `error`,
        confirmButtonColor: `#dc3545`,
      });
    }
  });
});

function removeAddToWatchlistButton(btnElement, movieId) {
  btnElement.style.display = `none`;
  document.querySelector(`button[data-movie-id="${movieId}"]`).dataset.isInWatchlist = `True`;
}
