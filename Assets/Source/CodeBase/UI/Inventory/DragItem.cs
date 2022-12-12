using SimpleRPG.Infrastructure;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
namespace SimpleRPG.UI
{
    public class DragItem<T> : GameEntity, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private IDragSource<T> source;
        private CanvasGroup canvasGroup;
        private Transform dragContainer;
        private Transform parent;
        private Vector3 startPosition;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Construct(IDragSource<T> source, Transform dragContainer)
        {
            this.source = source;
            this.dragContainer = dragContainer;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPosition = transform.position;
            parent = transform.parent;
            transform.SetParent(dragContainer);
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = startPosition;
            transform.SetParent(parent);
            canvasGroup.blocksRaycasts = true;
            IDragDestination<T> destination;
            if (EventSystem.current.IsPointerOverGameObject() && eventData.pointerEnter != null)
            {
                if (TryGetDestination(eventData.pointerEnter, out destination))
                {
                    DropItemInDestination(destination);
                }
            }
        }

        private void DropItemInDestination(IDragDestination<T> destination)
        {
            if (object.ReferenceEquals(destination, source))
                return;
            var sourceContainer = source as IDragContainer<T>;
            var destinationContainer = destination as IDragContainer<T>;
            if (destinationContainer == null || sourceContainer == null || destinationContainer.GetItem() == null ||
                object.ReferenceEquals(sourceContainer.GetItem(), destinationContainer.GetItem()))
            {
                AddItemToDestination(destination);
            }
            else
            {
                SwapItems(sourceContainer, destinationContainer);
            }
        }

        private void SwapItems(IDragContainer<T> sourceContainer, IDragContainer<T> destinationContainer)
        {
            var sourceItem = sourceContainer.GetItem();
            var souceCount = sourceContainer.GetCount();
            var destinationItem = destinationContainer.GetItem();
            var destinationCount = destinationContainer.GetCount();

            if (sourceContainer.MaxAcceptable(destinationItem) < destinationCount || destinationContainer.MaxAcceptable(sourceItem) < souceCount)
                return;

            sourceContainer.RemoveItem(souceCount);
            destinationContainer.RemoveItem(destinationCount);

            sourceContainer.AddItem(destinationItem, destinationCount);
            destinationContainer.AddItem(sourceItem, souceCount);
        }

        private void AddItemToDestination(IDragDestination<T> destination)
        {
            var draggingItem = source.GetItem();
            var draggingCount = source.GetCount();

            var acceptable = destination.MaxAcceptable(draggingItem);
            var toTransfer = Mathf.Min(draggingCount, acceptable);
            if (toTransfer > 0)
            {
                source.RemoveItem(toTransfer);
                destination.AddItem(draggingItem, toTransfer);
            }
        }

        private bool TryGetDestination(GameObject point, out IDragDestination<T> destination)
        {
            if (point.TryGetComponent(out destination))
                return true;
            if (point.transform.parent.TryGetComponent(out destination))
                return true;

            return false;
        }
    }
}
