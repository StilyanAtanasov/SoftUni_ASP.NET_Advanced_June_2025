document.addEventListener("DOMContentLoaded", function () {
    const buyTicketButtons = document.querySelectorAll(".buy-ticket-btn");

    buyTicketButtons.forEach(button => {
        button.addEventListener("click", function () {
            // Get movie and cinema data from button's data attributes
            const cinemaId = this.dataset.cinemaId;
            const movieId = this.dataset.movieId;
            const movieName = this.dataset.movieName;

            // Populate modal fields (adjust selectors as needed)
            document.querySelector("#buyTicketModal #movieNamePlaceholder").textContent = movieName;
            document.querySelector("#buyTicketModal #movieId").value = movieId;
            document.querySelector("#buyTicketModal #cinemaId").value = cinemaId;

            // Show the modal
            const buyTicketModal = new bootstrap.Modal(document.getElementById("buyTicketModal"));
            buyTicketModal.show();
        });
    });
});

document.getElementById('buyTicketButton').addEventListener('click', async function () {
    const cinemaId = document.getElementById('cinemaId').value;
    const movieId = document.getElementById('movieId').value;
    const quantity = parseInt(document.getElementById('quantity').value, 10);
    const errorMessage = document.getElementById('errorMessage');

    // Reset error
    errorMessage.classList.add('d-none');
    errorMessage.textContent = '';

    // Validate input
    if (!cinemaId || !movieId || isNaN(quantity) || quantity < 1) {
        errorMessage.textContent = 'Please enter a valid ticket quantity.';
        errorMessage.classList.remove('d-none');
        return;
    }

    try {
        const response = await fetch('/api/TicketApi/BuyTicket', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                cinemaId,
                movieId,
                quantity
            })
        });

        const data = await response.json();

        if (response.ok) {
            // Hide modal
            const modalElement = document.getElementById('buyTicketModal');
            const modalInstance = bootstrap.Modal.getInstance(modalElement);
            modalInstance.hide();

            // SweetAlert success
            Swal.fire({
                title: 'Purchase Successful!',
                text: `${quantity} ticket(s) purchased successfully.`,
                icon: 'success',
                confirmButtonColor: '#28a745'
            });
        } else {
            throw new Error(data.message || 'Failed to purchase ticket.');
        }
    } catch (err) {
        Swal.fire({
            title: 'Error',
            text: err.message,
            icon: 'error',
            confirmButtonColor: '#dc3545'
        });
    }
});