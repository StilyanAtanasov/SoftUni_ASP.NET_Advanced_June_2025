function filterCinemas() {
  const searchInput = document.getElementById("searchBar");
  const citySelect = document.getElementById("cityFilter");
  const cinemaContainer = document.getElementById("cinemaContainer");

  const searchTerm = searchInput.value.trim().toLowerCase();
  const selectedCity = citySelect.value.trim().toLowerCase();

  // Get all cinema cards
  const cinemaCards = cinemaContainer.getElementsByClassName("cinema-card");

  Array.from(cinemaCards).forEach(card => {
    const cinemaName = card.querySelector(".card-title").textContent.toLowerCase();
    const cinemaCity = card.getAttribute("data-city").toLowerCase();

    const matchesSearch = cinemaName.includes(searchTerm);
    const matchesCity = selectedCity === "" || cinemaCity === selectedCity;

    // Show or hide card based on filters
    if (matchesSearch && matchesCity) {
      card.style.display = "";
    } else {
      card.style.display = "none";
    }
  });
}

document.addEventListener("DOMContentLoaded", () => {
  filterCinemas();
});
