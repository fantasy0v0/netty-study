package com.github.fantasy0v0.netty;

import io.netty.buffer.ByteBuf;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;

public class DiscardServerHandler extends SimpleChannelInboundHandler<ByteBuf> {

  @Override
  protected void channelRead0(ChannelHandlerContext ctx, ByteBuf msg) throws Exception {
    msg.retain();
    new Thread(() -> {
      try {
        Thread.sleep(3000);
        ctx.executor().execute(() -> {
          ctx.writeAndFlush(msg);
        });
      } catch (InterruptedException e) {
        throw new RuntimeException(e);
      }
    }).start();
  }
}
