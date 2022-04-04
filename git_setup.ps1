git init --initial-branch=main
git add .
git commit -m "Initial commit using the developer pack script"
git remote add origin https://thefoschinigroup.visualstudio.com/DA/_git/Tfg%20Template%20Demo
git push -u origin --all
git branch development
git checkout development
git push -u origin development

echo . >> TFGTemplateDemo.sln