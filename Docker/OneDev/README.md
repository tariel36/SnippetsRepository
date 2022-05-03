# Abstract
This docker package creates container with [OneDev](https://onedev.io). Be aware that this package is designed for at home use.

# Details
## OneDev
* URL: `http://localhost:6610/`;

# Installation
1. Set environmental variable `relative_local_dir` in `.env` file - use `.env.template` as template. It has to be relative path, see example;
2. Run `install.bat`;

The `relative_local_dir` is a path to the directory on your local system where all the files are stored (so it includes git repositories). It is wise to set it to a directory that will not be deleted by an accident. Backups are also advised. See [this](https://code.onedev.io/projects/162/files/main/pages/backup-restore.md) official documentation for details.

Example .env file:
```
relative_local_dir=../../../../../_smietnik/temp
```

# External
* [OneDev](https://onedev.io);
