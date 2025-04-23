FROM python:3.12-alpine

RUN adduser -D appuser

WORKDIR /app
COPY requirements.txt .
RUN pip install --no-cache-dir --upgrade pip setuptools && \
    pip install --no-cache-dir -r requirements.txt

COPY server.py .

RUN chown -R appuser:appuser /app

USER appuser

EXPOSE 8000

CMD ["python", "server.py"]