

## Upload large file to git, using LFS : 
- In terminal inside project, Before you add or commit any large file (>50MB or > 100MB), install lfs : 
```
git lfs install
git lfs track "*.so"  // example for .so file
git lfs track "*.bundle"
git add .gitattributes  
// After that, you can add and commit, push like normallly 
git add . 
git commit -m "+ xxx"
git push 
```