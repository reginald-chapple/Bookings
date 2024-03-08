function insertPostData(response) {
    var post = $("#feed-posts").prepend(`
        <div class="card mb-4">
            <div class="card-body p-3 p-sm-4">
                <div class="border-bottom mb-3">
                    <div class="d-flex align-items-center mb-3">
                        <a href="#!">
                            <div class="avatar avatar-xl  me-2">
                                <img class="rounded-circle " src="${ response.authorImage }" alt="">
                            </div>
                        </a>
                        <div class="flex-1">
                            <a class="fw-bold mb-0 text-black" href="#!">
                                ${ response.author }
                            </a>
                            <p class="fs--2 mb-0 text-600 fw-semi-bold">Just now.</p>
                        </div>
                        <div class="btn-reveal-trigger">
                            <button class="btn btn-sm dropdown-toggle dropdown-caret-none transition-none d-flex btn-reveal"
                                type="button" data-bs-toggle="dropdown" data-boundary="window" aria-haspopup="true"
                                aria-expanded="false" data-bs-reference="parent">
                                <span class="fas fa-ellipsis-h"></span>
                            </button>
                            <div class="dropdown-menu dropdown-menu-end py-2">
                                <a class="dropdown-item" href="#!">Edit</a>
                                <a class="dropdown-item text-danger" href="#!">Delete</a>
                                <a class="dropdown-item" href="#!">Download</a>
                                <a class="dropdown-item" href="#!">Report abuse</a>
                            </div>
                        </div>
                    </div>
                    <p class="text-800">
                    ${ response.body }
                    </p>
                </div>
                <div class="d-flex justify-content-between">
                    <button class="btn btn-link p-0 me-3 fs--2 fw-bolder" type="button">
                        <span class="fa-solid fa-heart me-1"></span>0 Likes
                    </button>
                    <button class="btn btn-link text-900 p-0 fs--2 me-3 fw-bolder" type="button">
                        <span class="fa-solid fa-comment me-1"></span>0 Comments
                    </button>
                    <button class="btn btn-link text-900 p-0 fs--2 me-2 fw-bolder" type="button">
                        <span class="fa-solid fa-share me-1"></span>0 shares
                    </button>
                </div>
            </div>
            <div class="bg-100 border-top p-3 p-sm-4">
                <div class="d-flex align-items-center">
                    <a href="#!">
                        <div class="avatar avatar-m  me-2">
                            <img class="rounded-circle " src="${ response.authorImage }" alt="">
                        </div>
                    </a>
                    <div class="flex-1">
                        <input class="form-control" type="text" placeholder="Add comment">
                    </div>
                </div>
            </div>
        </div>
    `);

    return post;
}

function savePost() {
    const postform = document.getElementById('post-form');

    postform.addEventListener('submit', (e) => {
        e.preventDefault();

        const form = e.target;
        const formData = new FormData(form);
        const url = "/Posts/Create";

        function successCallback(response) {
            form.reset();
            insertPostData(response);
            console.log(response.message);
        }

        function errorCallback(xhr, status, error) {
            form.reset();
            console.log(error);
        }

        ajaxFormPost(url, formData, successCallback, errorCallback);
    });
}