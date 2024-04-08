function campaignTableRow(data) {
    var tableRow = $("#campaign-table-body").append(`
        <tr>
            <td class="fs--1 align-middle">
                <div class="form-check mb-0 fs-0">
                    <input class="form-check-input" type="checkbox">
                </div>
            </td>
            <td class="align-middle ps-3">
                <a href="/Campaigns/${data.slug}/Details">${data.name}</a> 
            </td>
            <td class="align-middle ps-3">${data.slug}</td>
            <td class="align-middle white-space-nowrap text-end pe-0">
                <div class="font-sans-serif btn-reveal-trigger position-static">
                    <button
                        class="btn btn-sm dropdown-toggle dropdown-caret-none transition-none btn-reveal fs--2"
                        type="button" data-bs-toggle="dropdown" data-boundary="window" aria-haspopup="true"
                        aria-expanded="false" data-bs-reference="parent">
                        <span class="bi bi-three-dots fs--2"></span>
                    </button>
                    <div class="dropdown-menu dropdown-menu-end py-2">
                        <a class="dropdown-item" href="#!">View</a>
                        <a class="dropdown-item" href="#">
                            Edit
                        </a>
                        <a class="dropdown-item" href="#!">Export</a>
                        <div class="dropdown-divider"></div>
                        <a class="dropdown-item text-danger" href="#!">Remove</a>
                    </div>
                </div>
            </td>
        </tr>
    `);

    return tableRow;
}

function create() {
    const form = document.getElementById("campaign-form");

    form.addEventListener("submit", function(event) {
        event.preventDefault();
        const formData = new FormData(event.target);
        const myModalEl = document.getElementById('saveCampaignModal');
        const modal = bootstrap.Modal.getInstance(myModalEl);
        const url = "/Campaigns/Create";

        function successCallback(response) {
            modal.hide();
            form.reset();
            campaignTableRow(response);
            flashToast("Your campaign has been created successfully.", "success");
        }

        function errorCallback(xhr, status, error) {
            modal.hide();
            form.reset();
            flashToast("Something went wrong.", "danger");
        }

        ajaxFormPost(url, formData, successCallback, errorCallback);

    });
}

const campaignActions = {
    create: create()
};
