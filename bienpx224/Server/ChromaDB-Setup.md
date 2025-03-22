# Hướng dẫn cài ChromaDB trên Ubuntu thông qua Docker : 

- Tải image ChromaDB mới nhất
docker pull chromadb/chroma:latest

- Chạy container
docker run -d \
  --name chroma \
  -p 8000:8000 \
  -e ALLOW_RESET=TRUE \
  -v chroma_data:/chroma/chroma \
  chromadb/chroma:latest