namespace Content.Features.AIModule.Scripts.Entity {
    public interface IEntity {
        public int ID { get; }
        public void SetBehaviour(IEntityBehaviour entityBehaviour);
    }
}